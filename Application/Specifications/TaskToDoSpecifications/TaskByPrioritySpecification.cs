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
    internal class TaskByPrioritySpecification: Specification<TaskToDo>
    {
        private readonly Priority _priority; 
        public TaskByPrioritySpecification(Priority priority)
        {
            _priority = priority;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) => task.Priority == _priority;

        }
    }
}
