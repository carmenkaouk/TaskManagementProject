using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReportingLineSpecification
{
    public class UserAndManagerReportingLineSpecification: Specification<ReportingLine>
    {
        private readonly int? _managerId;
        private readonly int? _subordinateId; 
        public UserAndManagerReportingLineSpecification(int? managerId, int?subordinateId)
        {
            _managerId = managerId;
            _subordinateId = subordinateId;
        }

        public override Expression<Func<ReportingLine, bool>> ToExpression()
        {
            return (reportingLine) => reportingLine.ManagerId == _managerId && reportingLine.SubordinateId==_subordinateId;

        }
    }
}
