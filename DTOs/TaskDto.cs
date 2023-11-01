using DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class TaskDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public DateTime AssignmentDate { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public Status Status { get; set; }
    [Required]
    public string AssigneeName { get; set; }    
}
