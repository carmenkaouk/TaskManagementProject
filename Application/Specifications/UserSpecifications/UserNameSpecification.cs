using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.UserSpecifications
{
    public class UserNameSpecification : Specification<User>
    {
        private readonly string _userName; 
        public UserNameSpecification(string userName)
        {
            _userName = userName;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Username == _userName;

        }
    }
}
