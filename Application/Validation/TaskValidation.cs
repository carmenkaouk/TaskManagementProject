using Application.Models;
using Application.Ports;
using Application.Validation.Interfaces;

namespace Application.Validation;

public class TaskValidation : ITaskValidation
{
    private readonly IRepository<TaskToDo> _taskRepository;
    private readonly IUserValidation _userValidation;
    public TaskValidation(IRepository<TaskToDo> taskRepository, IUserValidation userValidation)
    {
        _taskRepository = taskRepository;
        _userValidation = userValidation;
    }
    public void ValidateExistence(int id)
    {
        var task = _taskRepository.GetById(id);
        if (task == null)
        {
            throw new Exception($"Task with id {id} does not exist");
        }
    }
    public void ValidateTaskIsOwnedByUser(int userId, int taskId)
    {
        _userValidation.ValidateExistence(userId);
        ValidateExistence(taskId);
        var task = _taskRepository.GetById(taskId);
        
        if (task.AssignedUserId != userId)
        {
            throw new Exception($"Task with id: {task.Id} does not belong to user with id: {userId}");
        }
    }
}
