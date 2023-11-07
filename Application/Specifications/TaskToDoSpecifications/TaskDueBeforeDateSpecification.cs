using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.TaskToDoSpecification
{
    public class TaskDueBeforeDateSpecification: Specification<TaskToDo>
    {
        private readonly DateTime _dueDate;
        public TaskDueBeforeDateSpecification(DateTime dueDate)
        {
            _dueDate = dueDate;
        }

        public override Expression<Func<TaskToDo, bool>> ToExpression()
        {
            return (task) => task.DueDate < _dueDate;

        }
    }
}
