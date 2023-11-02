
using DTOs.Enums;
using DTOs;
using Application.Models;

namespace Application.Services.Interfaces;

public interface ITaskService
{
    Task CreatePersonalTask( NewTaskDto task);
    Task AssignTask(int managerId, NewTaskDto task);
    public List<TaskToDo> GetAllTasksOfSubordinates(int mangerId);
    List<TaskDto> GetTasksOfUser(int userId);
    Task SetTaskStatus(int userId, int taskId, Status status);
}