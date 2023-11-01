using System.ComponentModel.DataAnnotations;


namespace Application.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    public int ManagerId { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public bool IsBlocked { get; set; }
    [Required]
    public UserRole Role { get; set; }
}
