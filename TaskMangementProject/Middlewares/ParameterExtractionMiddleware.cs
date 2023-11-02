using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Presentation.Support;
using RequestResponse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares
{
    internal class ParameterExtractionMiddleware : Middleware
    {
        public override Response ProcessRequest(FileContext fileContext)
        {
            ArrayList parameters = new ArrayList();
            ParameterInfo[] parameterInfo = ((MethodInfo)fileContext.data["MethodInfo"]).GetParameters();
            foreach (var paramInfo in parameterInfo)
            {
                Type type = paramInfo.ParameterType;
                var content = ((Request)fileContext.data["Request"]).Content; 
                if (content[paramInfo.Name] is JObject jObjectParameter)
                {
                    object deserializedParameter = JsonConvert.DeserializeObject(jObjectParameter.ToString(), type);
                    parameters.Add(deserializedParameter);
                }
                else if (type.IsEnum)
                {
                    parameters.Add(Enum.ToObject(type, content[paramInfo.Name]));
                }
                else
                {
                    parameters.Add(Convert.ChangeType(content[paramInfo.Name], type));
                }
            }
            fileContext.Add("Parameters", parameters.ToArray()); 
            return _next.ProcessRequest(fileContext);
        }
    }
}
