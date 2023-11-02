using Application.Models;
using Application.Ports;
using Application.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IDepartmentValidation _departmentValidation;

        public DepartmentService(IRepository<Department> departmentRepository, IDepartmentValidation departmentValidationService)
        {
            _departmentRepository = departmentRepository;
            _departmentValidation = departmentValidationService;
        }

        public List<Department> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(department => new Department { Name = department.Name }).ToList();
        }
        public async Task CreateDepartment(string departmentName)
        {
            _departmentValidation.ValidateDepartmentName(departmentName);
            Department department = new Department { Name = departmentName };
            _departmentRepository.Add(department);
            await _departmentRepository.SaveChangesAsync();
        }

    }
}
