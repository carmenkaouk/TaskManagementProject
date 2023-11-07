using Application.Models;
using Application.Ports;
using Application.Services;
using Application.Services.Interfaces;
using Application.Specifications.DepartmentSpecifications;
using Application.Validation.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServicesTests
{
    [TestFixture]
    public class DepartmentServiceTest
    {
        private Mock<IRepository<Department>> departmentRepositoryMock;
        private Mock<IDepartmentValidation> departmentValidationMock;
        private DepartmentService departmentService;

        [SetUp]
        public void SetUp()
        {
            departmentRepositoryMock = new Mock<IRepository<Department>>();
            departmentValidationMock = new Mock<IDepartmentValidation>();
            departmentService = new DepartmentService(departmentRepositoryMock.Object, departmentValidationMock.Object);
        }

        [Test]
        public void GetAllDepartmentsTest()
        {
            departmentRepositoryMock.Setup(dep => dep.GetAll()).Returns(new List<Department>
            {
              new Department { Id = 1, Name = "It" },
              new Department { Id = 2, Name = "Management" }
            });
            List<Department> departments = departmentService.GetAllDepartments();

            Assert.That(departments.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetDepartmentsByNameTest()
        {
            var depName = "It";
            var departmentSpecification = new DepartmentNameSpecification(depName);

            var expectedDepartments = new List<Department>
            {
                new Department { Id = 1, Name = "It" },
                new Department { Id = 2, Name = "It" }
            };

            departmentRepositoryMock
                .Setup(dep => dep.Filter(It.Is<DepartmentNameSpecification>(spec => spec.GetType() == departmentSpecification.GetType())))
                .Returns(expectedDepartments);

            List<Department> departments = departmentService.GetAllDepartmentsByName(depName);
            
            Assert.IsNotNull(departments);
            Assert.AreEqual(2, departments.Count);
        }


            [Test]
        public void CreateDepartmentTest() 
        {
            string departmentName = "It";
            departmentValidationMock.Setup(v => v.ValidateDepartmentName(departmentName));
            departmentRepositoryMock.Setup(dep => dep.Add(It.IsAny<Department>()));
            departmentRepositoryMock.Setup(dep => dep.SaveChangesAsync()).Returns(Task.CompletedTask);

            departmentService.CreateDepartment(departmentName);

            departmentValidationMock.Verify(val => val.ValidateDepartmentName(departmentName));
            departmentRepositoryMock.Verify(dep => dep.Add(It.IsAny<Department>()), Times.Once);
            departmentRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
