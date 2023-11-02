using Client.Interfaces;
using DTOs;
using RequestResponse;
using RequestResponse.Enums;

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
        Request request = new Request() { Uri = "User/Login", MethodType = RequestType.Put, SenderUsername = username };
        request.Content.Add("username", password);
        request.Content.Add("password", password);
        var response = await _requestSender.SendRequest(request);
        return ((UserDto)response.Content);
    }




}
