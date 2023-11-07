using Client.Controllers;
using Client.Interfaces;
using Client.RequestSender;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.FileService;
using System.Reflection;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var serviceProvider = new ServiceCollection()
            .AddSingleton<IRequestSender, FileRequestSender>()
            .AddSingleton<IFileService, FileService>()
            .AddTransient<UserController, UserController>() 
            .AddSingleton(config)
            .BuildServiceProvider();
Type type = Assembly.GetExecutingAssembly().GetType("Client.Controllers.UserController");
var userController =(UserController) serviceProvider.GetService(type);
var dto = await userController.Login("fatouma", "lolo"); 