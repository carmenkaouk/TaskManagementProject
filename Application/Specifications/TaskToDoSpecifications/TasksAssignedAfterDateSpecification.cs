using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.TaskToDoSpecification
{
    internal class TasksAssignedAfterDateSpecification: Specification<TaskToDo>
    {
        private readonly DateTime _assignmentDate;
        public TasksAssignedAfterDateSpecification(DateTime assignmentDate)
        {
            _assignmentDate = assignmentDate;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) => task.AssignmentDate > _assignmentDate;

        }
    }
}
