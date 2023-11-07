using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.TaskToDoSpecification
{
    public class TaskByAssigneeSpecification: Specification<TaskToDo>
    {
        private readonly int _assigneeId; 
        public TaskByAssigneeSpecification(int assigneeId)
        {
            _assigneeId = assigneeId;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) =>  task.AssigneeId == _assigneeId;

        }
    }
}
