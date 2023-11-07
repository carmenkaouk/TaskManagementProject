using Application.Models;
using Application.Specifications;
using Application.Specifications.DepartmentSpecifications;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using Persistence.Repositories;

namespace Persistence.Tests
{
    [TestFixture]
    public class FileDepartmentRepositoryTest
    {
        private Mock<AbstractDatabaseManager> databaseManagerMock;
        private FileDepartmentRepository departmentRepository;

        [SetUp]
        public void SetUp()
        {
            databaseManagerMock = new Mock<AbstractDatabaseManager>();
            databaseManagerMock.Object.Departments = new List<Department>();
            departmentRepository = new FileDepartmentRepository(databaseManagerMock.Object);
        }

        [Test]
        public void AddDepartmentTest()
        {
            var department = new Department { Id = 1, Name = "It" };
            departmentRepository.Add(department);
            
            Assert.Contains(department, databaseManagerMock.Object.Departments);
        }

        [Test]
        public void GetAllDepartmentsTest() {
            var department = new Department { Id = 1, Name = "It" };
            departmentRepository.Add(department);
            var departments = departmentRepository.GetAll();
            Assert.That(departments.Count, Is.EqualTo(1));
        }

        [Test]

        public void GetDepartmentByIdTest() {
            var department = new Department { Id = 1, Name = "It" };
            departmentRepository.Add(department);
            
            var returnedDepartment = departmentRepository.GetById(1);
            Assert.That(department,Is.EqualTo(returnedDepartment));
        }

        [Test]
        public void UpdateDepartmentTest()
        {
            var department = new Department { Id = 1, Name = "It" };
            var updatedDepartment = new Department { Id = 1, Name = "Updated IT" };

            departmentRepository.Add(department);

            departmentRepository.Update(updatedDepartment);

            var retrievedDepartment = departmentRepository.GetById(1);

            Assert.IsNotNull(retrievedDepartment);
            Assert.AreEqual(updatedDepartment.Id, retrievedDepartment.Id);
            Assert.AreEqual(updatedDepartment.Name, retrievedDepartment.Name);

            var departments = departmentRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        [Test]
        public void FilterDepartmentTest() {

            var department1 = new Department { Id = 1, Name = "IT" };
            var department2 = new Department { Id = 2, Name = "HR" };
            var department3 = new Department { Id = 3, Name = "Finance" };

            departmentRepository.Add(department1);
            departmentRepository.Add(department2);
            departmentRepository.Add(department3);

            var specification = new DepartmentNameSpecification("HR");

            var filteredDepartments = departmentRepository.Filter(specification);


            Assert.That(filteredDepartments.Count, Is.EqualTo(1));
        
        }
        

     [Test]
        public void GetNextDepartmentIdTest() { 
            var id = departmentRepository.GetNextId();
            Assert.That(id,Is.EqualTo(1));
            var department = new Department { Id = 1, Name = "It" };
            departmentRepository.Add(department);
            var nextId = departmentRepository.GetNextId();
            Assert.That(nextId, Is.EqualTo(2));
        }

        [Test]
        public async Task SaveChangesAsyncTest()
        {
            var department = new Department { Id = 1, Name = "It" };
            departmentRepository.Add(department);

            await departmentRepository.SaveChangesAsync();

            databaseManagerMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }


    }
}
