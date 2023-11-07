using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.DepartmentSpecifications
{
    public class DepartmentNameSpecification: Specification<Department>
    {
        private readonly string _departmentName;
        public DepartmentNameSpecification(string departmentName)
        {
            _departmentName = departmentName;
        }

        public override Expression<Func<Department, bool>> ToExpression()
        {
            return (department) => department.Name == _departmentName;
        }
    }
}
