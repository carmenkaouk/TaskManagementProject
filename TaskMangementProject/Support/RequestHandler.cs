using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RequestResponse;
using SharedLibrary.FileService;
using RequestResponse.Enums; 
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Runtime.Intrinsics.Arm;
using SharedLibrary.Encryption;
using System.Text.Unicode;
using System.Text;

namespace Presentation.Support;

public class RequestHandler
{
    public MiddleWareManager _middleWareManager;
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IServiceProvider _serviceProvider;
    public RequestHandler(MiddleWareManager middleWareManager, IConfiguration configuration, IFileService fileService, IServiceProvider serviceProvider)
    {
        _middleWareManager = middleWareManager;
        _configuration = configuration;
        _fileService = fileService;
        _serviceProvider = serviceProvider;
    }
    public FileSystemWatcher WatchRequests()
    {
        var requestFolderPath = _configuration["FileDirectories:Requests"];
        _fileService.ValidatePath(requestFolderPath);
        var watchRequestFile = new FileSystemWatcher(requestFolderPath!);
        watchRequestFile.IncludeSubdirectories = false;
        watchRequestFile.Created += HandleRequests;
        watchRequestFile.EnableRaisingEvents = true;
        return watchRequestFile;
    }

    private async void HandleRequests(object sender, FileSystemEventArgs e)
    {
        Response response= new();
        FileContext fileContext= new(); 
        try
        {
            var content = await ReadFile(e);
            var headMiddleWare = _middleWareManager.ConstructPipeline();
            fileContext = new FileContext();
            fileContext.Add("JsonWrappedRequest", content);
            fileContext.Add("ServiceProvider", _serviceProvider);


            response = headMiddleWare.ProcessRequest(fileContext);
        }
        catch (Exception ex)
        {
            response.StatusCode = StatusCodes.Exception; 
            response.ExceptionMessage = ex.Message;
            Console.Error.WriteLine($"Error reading file: {ex.Message}");
        }
        finally
        {
            var encryptedResponse = EncryptResponse(response, (byte[])fileContext.data["SymmetricKey"],(byte[])fileContext.data["Iv"] ); 
            await WriteResponse(encryptedResponse, ( (Request)fileContext.data["Request"]).SenderUsername,response.RequestId);
        }

    }

    private byte[] EncryptResponse(Response response, byte[] key, byte[] iv)
    {
        var responseAsJson = JsonConvert.SerializeObject(response);
        byte[] responseASBytes= Encoding.UTF8.GetBytes(responseAsJson);
        return AesEncryption.Encrypt(responseASBytes, key, iv); 
    }

    private async Task<string> ReadFile(FileSystemEventArgs e)
    {
        string content = string.Empty; 
        while (true)
        {
            try
            {
                content = await _fileService.ReadFile(e.FullPath);
                break;
            }
            catch (IOException ex)
            {
                await Task.Delay(100);
            }
        }
        return content; 
    }

    private async Task WriteResponse(byte[] response,string username, Guid requestId )
    {
        try
        {
            string filePath;
            var responseFolderPath = _configuration["FileDirectories:Responses"];
            string fileName = requestId.ToString();
            if (responseFolderPath != null)
            {
                 filePath = Path.Combine(responseFolderPath, username, fileName);
                 
                await _fileService.WriteByteArray(filePath, response);
            }
           

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex); 
        }
    }
}




