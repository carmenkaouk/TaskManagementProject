using Application.Models;
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
            departmentRepository = new FileDepartmentRepository(databaseManagerMock.Object);
        }

        [Test]
        public void AddDepartmentTest()
        {
            var department = new Department { Id = 1, Name = "It" };
            
            departmentRepository.Add(department);
           
            Assert.Contains(department, databaseManagerMock.Object.Departments);
        }
    }
}
