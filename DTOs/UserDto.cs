using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class UserDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string DepartmentName { get; set; }
    public string? ManagerName { get; set; }
}
