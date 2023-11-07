using Application.Models;
using Application.Specifications;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using Persistence.Repositories;
using static DTOs.Enums.UserRole;

namespace Persistence.Tests
{
    public class FileUserRepositoryTest
    {

        private Mock<AbstractDatabaseManager> databaseManagerMock;
        private FileUserRepository userRepository;

        [SetUp]
        public void SetUp()
        {
            databaseManagerMock = new Mock<AbstractDatabaseManager>();
            databaseManagerMock.Object.Users = new List<User>();
            userRepository = new FileUserRepository(databaseManagerMock.Object);
        }


        [Test]
        public void AddUserTest()
        {
            var user = new User {
                Id = 2,
                Username = "Test",
                FirstName ="Joe",
                LastName ="Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash ="boo",
                IsBlocked = false,
                Role  = Employee
            };

            userRepository.Add(user);

            Assert.Contains(user, databaseManagerMock.Object.Users);
        }

        [Test]
        public void GetAllUsersTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            userRepository.Add(user);
            var users = userRepository.GetAll();
            Assert.That(users.Count, Is.EqualTo(1));
        }

        [Test]

        public void GetUserByIdTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            userRepository.Add(user);

            var returnedDepartment = userRepository.GetById(2);
            Assert.That(user, Is.EqualTo(returnedDepartment));
        }

        [Test]
        public void UpdateUserFirstNameTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joeee",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.FirstName, retrievedUser.FirstName);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }

        [Test]
        public void UpdateUserLastNameTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doee",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.LastName, retrievedUser.LastName);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }

        [Test]
        public void UpdateUserTitleTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Senior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.Title, retrievedUser.Title);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }
        [Test]
        public void UpdateUserDeparmentTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 2,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.DepartmentId, retrievedUser.DepartmentId);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }
        [Test]
        public void UpdateUserManagerTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joeee",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 3,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.ManagerId, retrievedUser.ManagerId);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }
        [Test]
        public void UpdateUserBlockedTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            var updatedUser = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joeee",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = true,
                Role = Employee
            };

            userRepository.Add(user);

            userRepository.Update(updatedUser);

            var retrievedUser = userRepository.GetById(2);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(updatedUser.Id, retrievedUser.Id);
            Assert.AreEqual(updatedUser.IsBlocked, retrievedUser.IsBlocked);

            var users = userRepository.GetAll();
            Assert.AreEqual(1, users.Count);

        }

        [Test]
        public void FilterDepartmentTest()
        {
           
        }
        
        [Test]
        public void GetNextUserIdTest()
        {
            var id = userRepository.GetNextId();
            Assert.That(id, Is.EqualTo(1));
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            userRepository.Add(user);
            var nextId = userRepository.GetNextId();
            Assert.That(nextId, Is.EqualTo(3));
        }

        [Test]
        public async Task SaveChangesAsyncTest()
        {
            var user = new User
            {
                Id = 2,
                Username = "Test",
                FirstName = "Joe",
                LastName = "Doe",
                Title = "Junior",
                DepartmentId = 1,
                ManagerId = 1,
                PasswordHash = "boo",
                IsBlocked = false,
                Role = Employee
            };
            userRepository.Add(user);

            await userRepository.SaveChangesAsync();

            databaseManagerMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }


    }
}
