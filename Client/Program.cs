using Client.Interfaces;
using Client.RequestSender;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.FileService;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var serviceProvider = new ServiceCollection()
            .AddSingleton<IRequestSender,FileRequestSender>()
            .AddSingleton<IFileService,FileService>()   
            .AddSingleton(config)
            .BuildServiceProvider();