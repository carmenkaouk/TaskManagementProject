using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Interfaces
{
    public interface ITaskValidation
    {
        void ValidateExistance(int id);
        void ValidateTaskOwner(int userId, int taskId);
    }
}
