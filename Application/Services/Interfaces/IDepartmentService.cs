using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        List<Department> GetAllDepartmentsByName(string departmentName);
        Task CreateDepartment(string departmentName);
    }
}
