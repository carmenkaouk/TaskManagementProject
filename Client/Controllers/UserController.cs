using Client.Interfaces;
using DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RequestResponse;
using RequestResponse.Enums;
using System.Text.Json.Serialization.Metadata;

namespace Client.Controllers;

public class UserController
{
    private readonly IRequestSender _requestSender;

    public UserController(IRequestSender requestSender)
    {
        _requestSender = requestSender;
    }

    public async Task<UserDto> Login(string username, string password)
    {
        Request request = new Request() { Uri = "User/Login", MethodType = RequestType.Post, SenderUsername = username };
        request.Content.Add("username", username);
        request.Content.Add("password", password);
        var response = await _requestSender.SendRequest(request);
        
        return (UserDto)((JObject)response.Content).ToObject(typeof(UserDto));
    }

   


}
