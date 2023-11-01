using Application.Models;
using Application.Ports;
using Application.Specifications.DepartmentSpecifications;
using Application.Validation.Interfaces;

namespace Application.Validation;


public class DepartmentValidation : IDepartmentValidation

{

    private readonly IRepository<Department> _departmentRepository;

    public DepartmentValidation(IRepository<Department> departmentRepository)

    {
        _departmentRepository = departmentRepository;
    }

    public void ValidateExistence(int departmentId)

    {

        var department = _departmentRepository.GetById(departmentId);

        if (department == null)

        {

            throw new Exception($"Department with id {departmentId} does not exist");

        }

    }

    public void ValidateDepartmentName(string departmentName)

    {
        var department = _departmentRepository.Filter(new DepartmentNameSpecification(departmentName));
        if (department != null)
        {
            throw new Exception($"Department with name {departmentName} already exists");
        }

    }

}
