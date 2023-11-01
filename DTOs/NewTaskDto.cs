using DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class NewTaskDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
}
