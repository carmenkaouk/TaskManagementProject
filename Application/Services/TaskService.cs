
using Application.Models;
using Application.Ports;
using Application.Services.Interfaces;
using Application.Specifications.TaskToDoSpecification;
using Application.Validation.Interfaces;
using DTOs;
using DTOs.Enums;
using System.Threading.Tasks;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly IRepository<TaskToDo> _taskRepository;
    private readonly ITaskValidation _taskValidation;
    private readonly IUserValidation _userValidation;

    public TaskService(IRepository<TaskToDo> taskRepository, ITaskValidation taskValidation, IUserValidation userValidation)
    {
        _taskRepository = taskRepository;
        _taskValidation = taskValidation;
        _userValidation = userValidation;
    }

    public async Task AssignTask(int managerId, NewTaskDto task)
    {
        _userValidation.ValidateExistence(managerId);
        _userValidation.ValidateExistence(task.UserId);
        _userValidation.ValidateIsManager(managerId);
        _userValidation.ValidateUserManager(managerId, task.UserId);

        TaskToDo newTask = new TaskToDo
        {
            Id = _taskRepository.GetNextId(),
            AssignedUserId = task.UserId,
            AssignmentDate = DateTime.Now,
            Description = task.Description,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = Status.Pending,
            Title = task.Title,
        };
        _taskRepository.Add(newTask);
        await _taskRepository.SaveChangesAsync();

    }

    public async Task CreatePersonalTask(NewTaskDto task)
    {
        _userValidation.ValidateExistence(task.UserId);

        TaskToDo newTask = new TaskToDo
        {
            Id = _taskRepository.GetNextId(),
            AssignedUserId = task.UserId,
            AssignmentDate = DateTime.Now,
            Description = task.Description,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = Status.Pending,
            Title = task.Title,
        };
        _taskRepository.Add(newTask);
        await _taskRepository.SaveChangesAsync();

    }

    public List<TaskDto> GetTasksOfUser(int userId)
    {
        _userValidation.ValidateExistence(userId);
        return _taskRepository.Filter(new TaskByUserSpecification(userId)).Select(task => new TaskDto()
        {
            AssignmentDate = task.AssignmentDate,
            DueDate = task.DueDate,
            Description = task.Description,
            Id = task.Id,
            Priority = task.Priority,
            Status = task.Status,
            Title = task.Title,
        }).ToList();
    }

    public async Task SetTaskStatus(int userId, int taskId, Status status)
    {
        _userValidation.ValidateExistence(userId);
        _taskValidation.ValidateExistence(taskId);
        _taskValidation.ValidateTaskIsOwnedByUser(userId, taskId);
        var task = _taskRepository.GetById(taskId);
        task.Status = status;
        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();
    }

    public List<TaskToDo> GetAllTasksOfSubordinates(int managerId)
    {
        _userValidation.ValidateIsManager(managerId);
        return _taskRepository.Filter(new TaskByAssigneeSpecification(managerId));
    }

}