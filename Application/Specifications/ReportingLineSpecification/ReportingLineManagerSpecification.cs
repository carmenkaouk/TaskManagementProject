using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReportingLineSpecification
{
    public class ReportingLineManagerSpecification : Specification<ReportingLine>
    {
        private readonly int _managerId ;
        public ReportingLineManagerSpecification(int managerId)
        {
            _managerId = managerId;
        }

        public override Expression<Func<ReportingLine, bool>> ToExpression()
        {
            return (reportingLine) => reportingLine.ManagerId == _managerId;

        }
    }
}
