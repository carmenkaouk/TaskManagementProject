using RequestResponse;

namespace Client.Interfaces;

public interface IRequestSender
{
    Response SendRequest(Request request);  
}
