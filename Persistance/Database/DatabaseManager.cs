using Application.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Persistence.Interfaces;
using SharedLibrary.FileService;
using System.Runtime.InteropServices;
using System.Threading;

namespace Persistence.Database;

public class DatabaseManager : AbstractDatabaseManager
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    public DatabaseManager(IFileService fileService, IConfiguration configuration)
    {
        _fileService = fileService;
        _configuration = configuration;
        var database = LoadAsync();
        Users = database.Result.Users;
        Departments = database.Result.Departments;
        Tasks = database.Result.Tasks;
        ReportingLines = database.Result.ReportingLines;
       
    }
    private async Task<Database> LoadAsync()
    {
        var databaseFilePath = _configuration["FileDirectories:Database"];
        string data = _fileService.ReadFile(databaseFilePath).GetAwaiter().GetResult();
        var database = JsonConvert.DeserializeObject<Database>(data);
        return database;
    }
    public override async Task SaveChangesAsync()
    {
        _semaphore.Wait();
        try
        {
            var databaseFilePath = _configuration["FileDirectories:Database"];
            string updatedDatabase = JsonConvert.SerializeObject(this, Formatting.Indented);
            await _fileService.WriteFile(databaseFilePath, updatedDatabase);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
