using Client.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RequestResponse;
using SharedLibrary.Encryption;
using SharedLibrary.FileService;
using System.IO;
using System.Text;

namespace Client.RequestSender;

public class FileRequestSender : IRequestSender
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly string _requestFolderPath;
    private readonly string _responseFolderPath;

    public FileRequestSender(IConfiguration configuration, IFileService fileService)
    {
        _configuration = configuration;
        _fileService = fileService;
        _requestFolderPath = _configuration["FileDirectories:Requests"];
        _responseFolderPath = _configuration["FileDirectories:Responses"];
    }
    public async Task<Response> SendRequest(Request request)
    {
        var responseTaskCompletionSource = new TaskCompletionSource<Response>();

        FileSystemWatcher watcher = null;
        try
        {
            (var requestString, var key, var iv) = PrepareRequest(request);

            watcher = WatchResponses(responseTaskCompletionSource, request.SenderUsername, key, iv);

            await _fileService.WriteFile(Path.Combine(_requestFolderPath, $"{request.RequestId}.json"), requestString);

            return await responseTaskCompletionSource.Task;
        }
        finally
        {
            watcher?.Dispose();
        }
    }
    private (string requestString, byte[] key, byte[] iv) PrepareRequest(Request request)
    {
        string content = JsonConvert.SerializeObject(request, Formatting.Indented);

        var keyAndIV = AesEncryption.GenerateSymmetricKey();

        var encryptedRequest = AesEncryption.Encrypt(Encoding.UTF8.GetBytes(content), keyAndIV.key, keyAndIV.IV);

        RsaEncryption.ImportPublicKey(GetServerPublicKey());
        WrappedRequest fileRequest = new WrappedRequest { EncryptedSymmetricKey = RsaEncryption.Encrypt(keyAndIV.key), Iv = keyAndIV.IV, EncryptedRequest = encryptedRequest };

        string wrappedRequestContent = JsonConvert.SerializeObject(fileRequest, Formatting.Indented);

        return (wrappedRequestContent, keyAndIV.key, keyAndIV.IV);
    }
    private byte[] GetServerPublicKey()
    {
        var publicKeyFile = _configuration["FileDirectories:PublicKey"];
        return File.ReadAllBytes(publicKeyFile);
    }
    private FileSystemWatcher WatchResponses(TaskCompletionSource<Response> tcs, string username, byte[] key, byte[] iv)
    {
        _fileService.CreateDirectoryIfMissing(_responseFolderPath + username);
        var watchResponseFile = new FileSystemWatcher(_responseFolderPath + username);
        watchResponseFile.IncludeSubdirectories = false;
        watchResponseFile.Created += async (sender, e) =>
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                Response receivedResponse = await ExtractResponse(e.FullPath, key, iv);
                tcs.TrySetResult(receivedResponse);
            }
        };
        watchResponseFile.EnableRaisingEvents = true;
        return watchResponseFile;
    }
    private async Task<Response> ExtractResponse(string path, byte[] key, byte[] iv)
    {
        bool isRead = false;
        byte[] encryptedResponse= null; 
        while (!isRead)
        {
            try
            {
                encryptedResponse = await _fileService.ReadByteArray(path);
                isRead = true;
            }
            catch
            {

            }
        }

        var response = AesEncryption.Decrypt(encryptedResponse, key, iv);
        return JsonConvert.DeserializeObject<Response>(Encoding.UTF8.GetString(response));
    }






}
