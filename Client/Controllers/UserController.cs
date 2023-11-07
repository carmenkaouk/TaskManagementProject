using Client.Interfaces;
using Client.Support;
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
        ResponseValidationService.ValidateResponse(response);
        return (UserDto)((JObject)response.Content).ToObject(typeof(UserDto));
    }

    public async Task CreateUser(NewUserDto user, string username)
    {
        Request request = new Request() { Uri = "User/CreateUser", MethodType = RequestType.Post, SenderUsername = username };
        request.Content.Add("user", user);
        var response = await _requestSender.SendRequest(request);
        ResponseValidationService.ValidateResponse(response);
    }

    public async Task<List<UserDto>> GetAllUsers(string username)
    {
        Request request = new Request() { Uri = "User/GetAllUsers", MethodType = RequestType.Get, SenderUsername = username };
        var response = await _requestSender.SendRequest(request);
        ResponseValidationService.ValidateResponse(response);
        return (List<UserDto>)((JArray)response.Content).ToObject(typeof(List<UserDto>));
    }
    public async Task BlockUser(int userId, string username)
    {
        Request request = new Request() { Uri = "User/BlockUser", MethodType = RequestType.Post, SenderUsername = username };
        request.Content.Add("userId", userId);
        var response = await _requestSender.SendRequest(request);
        ResponseValidationService.ValidateResponse(response);
    }

    public async Task ChangePassword(int userId, string newPassword, string username)
    {
        Request request = new Request() { Uri = "User/ChangePassword", MethodType = RequestType.Post, SenderUsername = username };
        request.Content.Add("userId", userId);
        request.Content.Add("newPassword", newPassword);
        var response = await _requestSender.SendRequest(request);
        ResponseValidationService.ValidateResponse(response);
    }

    public async Task<List<UserDto>> GetUsersByDepartment(int departmentId, string username)
    {
        Request request = new Request() { Uri = "User/GetUsersByDepartment", MethodType = RequestType.Get, SenderUsername = username };
        request.Content.Add("departmentId", departmentId);
        var response = await _requestSender.SendRequest(request);
        ResponseValidationService.ValidateResponse(response);
        return (List<UserDto>)((JArray)response.Content).ToObject(typeof(List<UserDto>));
    }
}
