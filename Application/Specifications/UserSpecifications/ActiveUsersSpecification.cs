using Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.UserSpecifications
{
    public class ActiveUsersSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.IsBlocked == false;

        }
    }
}
