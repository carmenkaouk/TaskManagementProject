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
    public class UserTitleSpecification:Specification<User>
    {
        private readonly string _title; 
        public UserTitleSpecification(string title)
        {
            _title = title;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Title == _title;

        }

    }
}
