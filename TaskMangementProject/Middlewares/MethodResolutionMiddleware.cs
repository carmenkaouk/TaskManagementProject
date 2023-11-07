using Microsoft.Extensions.DependencyInjection;
using Presentation.Support;
using RequestResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares; 

internal class MethodResolutionMiddleware : Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {
        var routePieces = GetRoute(((Request)fileContext.data["Request"]).Uri);
        var controllerName = routePieces.ServiceName;
        var methodName = routePieces.MethodName;

        Type controllerType = Assembly.GetExecutingAssembly().GetType("Presentation.Controllers." + controllerName + "Controller"); 
        if (controllerType == null || !controllerType.IsClass)
        {
            throw new Exception("Controller not found");
        }

        object? controllerInstance = ((IServiceProvider)fileContext.data["ServiceProvider"]).GetService(controllerType);

        MethodInfo targetMethod = controllerType.GetMethod(methodName)!;
        if (targetMethod == null)
        {
            throw new Exception("Method not found");
        }
        fileContext.Add("ControllerInstance", controllerInstance);
        fileContext.Add("MethodInfo", targetMethod);
        return _next.ProcessRequest(fileContext);


    }
    private (string ServiceName, string MethodName) GetRoute(string requestType)
    {
        var route = requestType.Split('/');
        if (route.Length == 2 && !string.IsNullOrWhiteSpace(route[0]) && !string.IsNullOrWhiteSpace(route[1]))
        {
            var serviceName = route[0];
            var methodName = route[1];
            return (serviceName, methodName);
        }
        else
        {
            throw new Exception("Invalid Route Format");
        }

    }
}
