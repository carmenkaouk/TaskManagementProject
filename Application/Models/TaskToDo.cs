using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class TaskToDo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public int AssignedUserId { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
    [Required]
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    [Required]
    public int AssigneeId { get; set; }
}
