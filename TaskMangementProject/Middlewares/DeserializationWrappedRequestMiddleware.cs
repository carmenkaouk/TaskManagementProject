using Newtonsoft.Json;
using Presentation.Support;
using RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Presentation.Middlewares; 

internal class DeserializationWrappedRequestMiddleware :Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {
        var wrappedRequest = JsonConvert.DeserializeObject<WrappedRequest>((string)fileContext.data["JsonWrappedRequest"]);
        fileContext.Add("WrappedRequest", wrappedRequest); 
        return _next.ProcessRequest(fileContext);
    }
}
