using Application.Models;

namespace Application.Services.Interfaces;

public interface IReportingLineService
{
    public void AddReportingLine(int userId, int? managerId);
    public void EndReportingLine(int userId, int managerId);
    public ReportingLine GetReportingLine(int userId, int managerId);
    public List<ReportingLine> GetAllManagersSubordinatesHistory(int managerId);
    public List<ReportingLine> GetAllReportingLines();
    public List<ReportingLine> GetAllActiveReporting();
}
