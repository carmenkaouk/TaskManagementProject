using Application.Models;
using Application.Ports;
using Application.Services.Interfaces;
using Application.Specifications.UserSpecifications;
using Application.Validation.Interfaces;
using DTOs;
using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IReportingLineService _reportingLineService;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IUserValidation _userValidation;
        private readonly IDepartmentValidation _departmentValidation;

        public UserService(IRepository<User> userRepository, ReportingLineService reportingLineService, IUserValidation userValidation, IDepartmentValidation departmentValidation, IRepository<Department> departmentRepository)
        {

            _userRepository = userRepository;
            _userValidation = userValidation;
            _departmentValidation = departmentValidation;
            _departmentRepository = departmentRepository;
            _reportingLineService = reportingLineService;

        }
        public async Task CreateUser(NewUserDto userInfo)
        {
            _userValidation.ValidateUsername(userInfo.Username);

            _departmentValidation.ValidateExistence(userInfo.DepartmentId);

            if (userInfo.ManagerId != null)
            {
                _userValidation.ValidateIsManager(userInfo.ManagerId);
            }

            User newUser = new User()
            {
                Id = _userRepository.GetNextId(),
                Username = userInfo.Username,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                DepartmentId = userInfo.DepartmentId,
                ManagerId = userInfo.ManagerId,
                Title = userInfo.Title,
                PasswordHash = HashingService.ComputeSHA256Hash(userInfo.Password),
                Role = userInfo.Role
            };

            _userRepository.Add(newUser);
            if (userInfo.ManagerId != null)
                _reportingLineService.AddReportingLine(newUser.Id, newUser.ManagerId);
            await _userRepository.SaveChangesAsync();
        }
        public List<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(user => new UserDto
            {
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                DepartmentName = _departmentRepository.GetById(user.DepartmentId).Name,
                ManagerName = _userRepository.GetById(user.ManagerId).FirstName + " " + _userRepository.GetById(user.ManagerId).LastName,
                Title = user.Title
            }).ToList();
        }
        public async Task ChangeUserDepartment(int userId, int departmentId)
        {
            _userValidation.ValidateExistence(userId);
            _departmentValidation.ValidateExistence(departmentId);
            var user = _userRepository.GetById(userId);
            user.DepartmentId = departmentId;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task BlockUser(int userId)
        {
            _userValidation.ValidateExistence(userId);
            var user = _userRepository.GetById(userId);
            user.IsBlocked = true;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task ChangeUserManager(int userId, int managerId)
        {
            _userValidation.ValidateExistence(userId);
            _userValidation.ValidateIsManager(managerId);

            var user = _userRepository.GetById(userId);
            if(user.ManagerId!= null)
            {
                _reportingLineService.EndReportingLine(userId, managerId); 

            }
            user.ManagerId = managerId;
            _userRepository.Update(user);
            if (managerId != null)
                _reportingLineService.AddReportingLine(userId, managerId);

            await _userRepository.SaveChangesAsync();
        }

        public async Task ChangePassword(int userId, string newPassword)
        {
            _userValidation.ValidateExistence(userId);
            var newPasswordHash = HashingService.ComputeSHA256Hash(newPassword);
            var user = _userRepository.GetById(userId);
            user.PasswordHash = newPasswordHash;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
        public List<UserDto> GetUsersByName(string name)
        {
            return _userRepository.Filter(new UserNameSpecification(name)).Select(user => new UserDto
            {
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                DepartmentName = _departmentRepository.GetById(user.DepartmentId).Name,
                ManagerName = _userRepository.GetById(user.ManagerId).FirstName + " " + _userRepository.GetById(user.ManagerId).LastName,
                Title = user.Title
            }).ToList();

        }
        public List<UserDto> GetUsersByTitle(string title)
        {
            return _userRepository.Filter(new UserTitleSpecification(title)).Select(user => new UserDto
            {
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                DepartmentName = _departmentRepository.GetById(user.DepartmentId).Name,
                ManagerName = _userRepository.GetById(user.ManagerId).FirstName + " " + _userRepository.GetById(user.ManagerId).LastName,
                Title = user.Title
            }).ToList();
        }
        public List<UserDto> GetUsersByDepartment(int departmentId)
        {
            return _userRepository.Filter(new UserDepartmentSpecification(departmentId)).Select(user => new UserDto
            {
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                DepartmentName = _departmentRepository.GetById(user.DepartmentId).Name,
                ManagerName = _userRepository.GetById(user.ManagerId).FirstName + " " + _userRepository.GetById(user.ManagerId).LastName,
                Title = user.Title
            }).ToList();
        }
        public List<UserDto> GetUsersByRole(UserRole role)
        {
            return _userRepository.Filter(new UserRoleSpecification(role)).Select(user => new UserDto
            {
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                DepartmentName = _departmentRepository.GetById(user.DepartmentId).Name,
                ManagerName = _userRepository.GetById(user.ManagerId).FirstName + " " + _userRepository.GetById(user.ManagerId).LastName,
                Title = user.Title
            }).ToList();
        }

    }
}
