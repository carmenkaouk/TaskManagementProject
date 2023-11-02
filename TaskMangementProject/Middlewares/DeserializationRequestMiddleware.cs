using Newtonsoft.Json;
using Presentation.Support;
using RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares; 

internal class DeserializationRequestMiddleware : Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {
        Request request = JsonConvert.DeserializeObject<Request>((string)fileContext.data["RequestAsJson"]);
        fileContext.Add("Request", request); 
        return _next.ProcessRequest(fileContext);
    }
}
