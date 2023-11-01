using Application.Models;

namespace Persistence.Interfaces;

public abstract class  AbstractDatabaseManager
{

    public List<User> Users { get; set; }
    public List<Department> Departments { get; set; }
    public List<TaskToDo> Tasks { get; set; }
    public List<ReportingLine> ReportingLines { get; set; }
    public abstract Task SaveChangesAsync();
}
