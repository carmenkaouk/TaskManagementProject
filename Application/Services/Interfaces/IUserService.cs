using DTOs;
using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUser(NewUserDto user);
        public List<UserDto> GetAllUsers();
        public Task BlockUser(int userId);
        public Task ChangePassword(int userId, string newPassword);
        public Task ChangeUserDepartment(int userId, int departmentId);
        public Task ChangeUserManager(int userId, int managerId);
        public List<UserDto> GetUsersByName(string name);
        public List<UserDto> GetUsersByTitle(string title);
        public List<UserDto> GetUsersByDepartment(int departmentId);
        public List<UserDto> GetUsersByRole(UserRole role);


    }
}
