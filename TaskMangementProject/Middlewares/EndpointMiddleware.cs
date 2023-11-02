using Presentation.Support;
using RequestResponse;
using System.Reflection;
using RequestResponse.Enums;

namespace Presentation.Middlewares;

public class EndpointMiddleware : Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {
        var controllerInstance = ((object?)fileContext.data["ControllerInstance"]);
        var parameters = ((object[])fileContext.data["Parameters"]);
        var targetMethod = ((MethodInfo)fileContext.data["MethodInfo"]);
        var result = targetMethod.Invoke(controllerInstance, parameters);
        object responseContent = result;
        if (result is Task taskResult)
        {
            taskResult.GetAwaiter().GetResult();
            responseContent = taskResult;
        }
        Response response = new Response { RequestId = ((Request)fileContext.data["Request"]).RequestId, StatusCode = StatusCodes.Success, Content = responseContent };
        return response;
    }
}
