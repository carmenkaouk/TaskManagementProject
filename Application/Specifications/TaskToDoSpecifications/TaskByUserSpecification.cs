using Application.Models;
using DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.TaskToDoSpecification
{
    internal class TaskByUserSpecification: Specification<TaskToDo>
    {
        private readonly int _userId;
        public TaskByUserSpecification(int userId)
        {
            _userId = userId;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) => task.AssignedUserId == _userId;

        }
    }
}
