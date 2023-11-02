using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ReportingLine
{
    public int Id { get; set; }
    [Required]
    public int? ManagerId { get; set; }
    [Required]
    public int SubordinateId { get; set; }
    [Required]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; set; } = DateOnly.MaxValue; 
}
