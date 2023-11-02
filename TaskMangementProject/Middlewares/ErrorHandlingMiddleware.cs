using Presentation.Support;
using RequestResponse;
using RequestResponse.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares;

internal class ErrorHandlingMiddleware : Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {

        try
        {
            return _next.ProcessRequest(fileContext);
        }
        catch (Exception e)
        {
            return new Response()
            {
                RequestId = ((Request)fileContext.data["Request"]).RequestId,
                StatusCode = StatusCodes.Exception,
                ExceptionMessage = e.Message
            };

        }
    }

}