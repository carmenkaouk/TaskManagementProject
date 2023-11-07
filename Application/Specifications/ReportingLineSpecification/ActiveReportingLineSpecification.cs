using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReportingLineSpecification
{
    public class ActiveReportingLineSpecification: Specification<ReportingLine>
    {
  
        public override Expression<Func<ReportingLine, bool>> ToExpression()
        {
            return (reportingLine) => reportingLine.EndDate > DateOnly.FromDateTime(DateTime.Now);
        }

    }
}
