using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ReportingLine
{
    public int Id { get; set; }
    [Required]
    public int ManagerId { get; set; }
    [Required]
    public int SubordinateId { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; } = DateOnly.MaxValue; 
}
