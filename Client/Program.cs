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


var dto = await userController.Login("fatouma", "touma");

Console.WriteLine(dto.Name + " " + dto.DepartmentName);

//await userController.CreateUser(new NewUserDto { Username = "ckaouk", FirstName = "Carmen", LastName = "Kaouk", DepartmentId = 1, Password = "256gt78ui", Role = DTOs.Enums.UserRole.Employee},"fatouma");
//var users = await userController.GetAllUsers("fatouma");
//await userController.BlockUser(2, "fatouma");
//await userController.ChangePassword(1, "touma","fatouma");
var users = await userController.GetUsersByDepartment(1, "fatouma");
foreach (var user in users)
{
    Console.WriteLine(user.Name + " "+user.DepartmentName);
}
