using RequestResponse.Enums;
using System.Runtime.InteropServices;

namespace RequestResponse;

public class Request

{

    public Guid RequestId { get; set; } = Guid.NewGuid();

    public string SenderUsername { get; set; }

    public string Uri { get; set; }

    public RequestType MethodType { get; set; }

    public Dictionary<string, Object> Header = new Dictionary<string, Object>()

        {

            {"Accept", null },

            {"Accept_Language", null  },

            { "Timeout" , null },

            {"Authentication" , null }

        };

    public Dictionary<string, object> Content = new Dictionary<string, object>();

    public Request()

    {

    }

    public Request(string uri, RequestType methodType, string senderUsername)

    {

        Uri = uri;

        MethodType = methodType;

        SenderUsername = senderUsername;

    }

    public Request(string uri, RequestType methodType, string senderUsername, Dictionary<string, object> content)

    {

        Uri = uri;

        MethodType = methodType;

        SenderUsername = senderUsername;

        Content = content;

    }

}
