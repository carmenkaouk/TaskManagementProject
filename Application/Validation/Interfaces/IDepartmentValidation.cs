using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Interfaces
{
    public interface IDepartmentValidation

    {
        void ValidateExistence(int departmentId);

        void ValidateDepartmentName(string departmentName);

    }
}
