using Application.Models;
using Application.Ports;
using Application.Specifications.UserSpecifications;
using Application.Validation.Interfaces;
using DTOs.Enums;


namespace Application.Validation;


public class UserValidation : IUserValidation
{
    private readonly IRepository<User> _userRepository;

    public UserValidation(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public void ValidateUsername(string username)
    {
        var user = _userRepository.Filter(new UsernameSpecification(username)).FirstOrDefault();
        if (user != null)
        {
            throw new Exception("Cannot use duplicate username");
        }
    }

    public void ValidateExistence(int? id)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            throw new Exception($"User with id {id} does not exist");
        }
    }
    public void ValidateIsManager(int? id)
    {
       ValidateExistence(id);
        var user =_userRepository.GetById(id);
        if ( user.Role != UserRole.Manager)
        {
            throw new Exception($"User with id: {id} is not a manager");
        }
    }
    public void ValidateUserManager(int managerID, int userID)
    {
        var user= _userRepository.GetById(userID);
        
        if (user.ManagerId != managerID)
        {
            throw new Exception($"User with id: {managerID} is not the manager of user with id: {userID}");
        }
    }
}
