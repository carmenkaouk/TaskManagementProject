using Application.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Persistence.Interfaces;
using SharedLibrary.FileService;
using System.Runtime.InteropServices;
using System.Threading;

namespace Persistence.Database;

public class Database : IDatabase
{
    public List<User> Users { get; set; }
    public List<Department> Departments { get; set; }
    public List<TaskToDo> Tasks { get; set; }
    public List<ReportingLine> ReportingLines { get; set; }
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    public Database(IFileService fileService, IConfiguration configuration)
    {
        _fileService = fileService;
        var database = LoadAsync();
        Users = database.Result.Users;
        Departments = database.Result.Departments;
        Tasks = database.Result.Tasks;
        ReportingLines = database.Result.ReportingLines;
        _configuration = configuration;
    }
    private async Task<Database> LoadAsync()
    {
        var databaseFilePath = _configuration["FileDirectories:Database"];
        string data = await _fileService.ReadFile(databaseFilePath);
        var database = JsonConvert.DeserializeObject<Database>(data)!;
        return database;
    }
    public async Task SaveChangesAsync()
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
