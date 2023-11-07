// See https://aka.ms/new-console-template for more information
using Application.Services.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers;
using SharedLibrary.FileService;
using Application.Ports;
using Application.Models;
using Persistence.Repositories;
using Application.Validation.Interfaces;
using Application.Validation;
using Microsoft.Extensions.Configuration;
using Presentation.Support;
using Infrastructure;
using System.Reflection;
using DTOs;
using Persistence.Interfaces;
using Persistence.Database;
using System.Security.Cryptography;
using SharedLibrary.Encryption;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var serviceProvider = new ServiceCollection()
            .AddSingleton<IFileService, FileService>()
            .AddSingleton<IRepository<User>, FileUserRepository>()
            .AddSingleton<IRepository<TaskToDo>, FileTaskRepository>()
            .AddSingleton<IRepository<Department>, FileDepartmentRepository>()
            .AddSingleton<IRepository<ReportingLine>, FileReportingLineRepository>()
            .AddSingleton<IHashingService, SHA256HashingService>()
            .AddTransient<UserController, UserController>()
            .AddSingleton<AbstractDatabaseManager, DatabaseManager>()
            .AddTransient<IReportingLineService, ReportingLineService>()
            //.AddTransient<TaskController, TaskController>()
            //.AddTransient<DepartmentController, DepartmentController>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<ITaskService, TaskService>()
            .AddTransient<IDepartmentService, DepartmentService>()
            .AddTransient<ITaskValidation, TaskValidation>()
            .AddTransient<IUserValidation, UserValidation>()
            .AddTransient<IDepartmentValidation, DepartmentValidation>()
            .AddSingleton(config)
            .BuildServiceProvider();

RequestHandler requestHandler = new RequestHandler(new MiddleWareManager(),config, new FileService(), serviceProvider);
var watcher = requestHandler.WatchRequests();
var fileService= (FileService)serviceProvider.GetService(typeof(IFileService));
await fileService.WriteByteArray("C:\\Root\\ServerPublicKey.txt", RsaEncryption.ExportPublicKey()); 
Console.ReadLine(); 
