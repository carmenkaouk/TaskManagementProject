using Application.Models;
using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.UserSpecifications
{
    public class UserRoleSpecification:Specification<User>
    {
        private readonly UserRole _role; 
        public UserRoleSpecification(UserRole role)
        {
            _role = role;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Role == _role;

        }
    }
}
