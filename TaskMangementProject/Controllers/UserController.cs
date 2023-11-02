using Application.Services.Interfaces;
using DTOs.Enums;
using DTOs;

namespace Presentation.Controllers;

public class UserController : BaseController
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public UserDto Login(string username, string password)
    {
       return _userService.Login(username, password);
    }
    public async Task CreateUser(NewUserDto user)
    {
        await _userService.CreateUser(user);
    }
    public List<UserDto> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }
    public async Task BlockUser(int userId)
    {
        await _userService.BlockUser(userId);
    }
    public async Task ChangePassword(int userId, string newPassword)
    {
        await _userService.ChangePassword(userId, newPassword);
    }
    public async Task ChangeUserDepartment(int userId, int departmentId)
    {
        await _userService.ChangeUserDepartment(userId, departmentId);
    }
    public async Task ChangeUserManager(int userId, int managerId)
    {
        await _userService.ChangeUserManager(userId, managerId);
    }
    public List<UserDto> GetUsersByName(string name)
    {
       return _userService.GetUsersByName(name);
    }
    public List<UserDto> GetUsersByTitle(string title)
    {
        return _userService.GetUsersByTitle(title);
    }
    public List<UserDto> GetUsersByDepartment(int departmentId)
    {
        return _userService.GetUsersByDepartment(departmentId);
    }
    public List<UserDto> GetUsersByRole(UserRole role)
    {
        return _userService.GetUsersByRole(role);
    }

}
