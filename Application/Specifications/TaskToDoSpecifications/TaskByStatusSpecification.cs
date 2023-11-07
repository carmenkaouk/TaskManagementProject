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
    public class TaskByStatusSpecification: Specification<TaskToDo>
    {
        private readonly Status _status; 
        public TaskByStatusSpecification(Status status)
        {
            _status = status;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) => task.Status == _status;

        }
    }
}
