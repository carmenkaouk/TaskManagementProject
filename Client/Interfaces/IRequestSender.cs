using RequestResponse;

namespace Client.Interfaces;

public interface IRequestSender
{
    Task<Response> SendRequest(Request request);  
}
