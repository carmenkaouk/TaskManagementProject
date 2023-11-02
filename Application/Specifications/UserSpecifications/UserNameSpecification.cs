using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.UserSpecifications
{
    public class UsernameSpecification : Specification<User>
    {
        private readonly string _username; 
        public UsernameSpecification(string username)
        {
            _username = username;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Username == _username;

        }
    }
}
