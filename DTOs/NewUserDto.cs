using DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class NewUserDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    public int? ManagerId { get; set; }
    [Required]
    public UserRole Role { get; set; }
}
