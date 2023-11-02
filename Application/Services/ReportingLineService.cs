using Application.Models;
using Application.Ports;
using Application.Services.Interfaces;
using Application.Specifications.ReportingLineSpecification;
using Application.Validation.Interfaces;

namespace Application.Services;

public class ReportingLineService : IReportingLineService
{
    private readonly IRepository<ReportingLine> _reportingLineRepository;
   

    public ReportingLineService(IRepository<ReportingLine> reportingLineRepository)
    {
        _reportingLineRepository = reportingLineRepository;
    }

    public void AddReportingLine(int userId, int? managerId)
    {
        _reportingLineRepository.Add(new ReportingLine
        {
            Id = _reportingLineRepository.GetNextId(),
            ManagerId = managerId,
            SubordinateId = userId,
        });
    }

    public ReportingLine GetReportingLine(int userId, int managerId) {
        return _reportingLineRepository.Filter(new UserAndManagerReportingLineSpecification(userId, managerId)).FirstOrDefault(); 
    }

    public void EndReportingLine(int userId, int managerId)
    {
        var reportingLine = GetReportingLine(userId, managerId);
        reportingLine.EndDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public List<ReportingLine> GetAllActiveReporting()
    {
        return _reportingLineRepository.Filter(new ActiveReportingLineSpecification());
    }

    public List<ReportingLine> GetAllManagersSubordinatesHistory(int managerId)
    {
        return _reportingLineRepository.Filter(new ReportingLineManagerSpecification(managerId));   
    }

    public List<ReportingLine> GetAllReportingLines()
    {
        return _reportingLineRepository.GetAll();
    }
}
