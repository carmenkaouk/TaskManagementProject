using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RequestResponse;
using SharedLibrary.FileService;
using System.Text.Json.Nodes;

namespace Presentation.Support;

public class RequestHandler
{
    public MiddleWareManager _middleWareManager;
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    public RequestHandler(MiddleWareManager middleWareManager, IConfiguration configuration, IFileService fileService)
    {
        _middleWareManager = middleWareManager;
        _configuration = configuration;
        _fileService = fileService;
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
        try
        {
            while (true)
            {
                try
                {
                    string content = await _fileService.ReadFile(e.FullPath);
                    var headMiddleWare = _middleWareManager.ConstructPipeline();
                    var fileContext = new FileContext();
                    fileContext.Add("JsonWrappedRequest", content);
                    Response response = headMiddleWare.ProcessRequest(fileContext);
                    break;
                }
                catch (IOException ex)
                {
                    Console.Write(ex);
                    await Task.Delay(100);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file: {ex.Message}");
        }
    }
    private void WriteResponse(Response response)
    {
        try
        {
            string filePath;
            var responseFolderPath = _configuration["FileDirectories:Responses"];
            string fileName = response.RequestId.ToString();
            if (responseFolderPath != null)
            {
                 filePath = Path.Combine(responseFolderPath, fileName);
                 string JsonResponse =  JsonConvert.SerializeObject(response);
                _fileService.WriteFile(filePath, JsonResponse);
            }
           

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}




