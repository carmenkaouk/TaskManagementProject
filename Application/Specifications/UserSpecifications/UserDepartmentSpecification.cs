using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.UserSpecifications
{
    public class UserDepartmentSpecification : Specification<User>
    {
        private readonly int _departmentId; 
        public UserDepartmentSpecification(int departmentId ) 
        {
            _departmentId = departmentId;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.DepartmentId==_departmentId;

        }
    }
}
