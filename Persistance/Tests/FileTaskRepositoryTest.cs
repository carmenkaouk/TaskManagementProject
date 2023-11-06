using Application.Models;
using DTOs.Enums;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tests
{
    public class FileTaskRepositoryTest
    {
        private Mock<AbstractDatabaseManager> databaseManagerMock;
        private FileTaskRepository taskRepository;

        [SetUp]
        public void SetUp()
        {
            databaseManagerMock = new Mock<AbstractDatabaseManager>();
            databaseManagerMock.Object.Tasks = new List<TaskToDo>();
            taskRepository = new FileTaskRepository(databaseManagerMock.Object);
        }

        [Test]
        public void AddDepartmentTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1, 
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending, 
                AssigneeId = 1 
            };

            taskRepository.Add(task);

            Assert.Contains(task, databaseManagerMock.Object.Tasks);
        }

        [Test]
        public void GetAllDepartmentsTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);
            var tasks = taskRepository.GetAll();
            Assert.That(tasks.Count, Is.EqualTo(1));
        }

        [Test]

        public void GetDepartmentByIdTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            var returnedTask = taskRepository.GetById(1);
            Assert.That(task, Is.EqualTo(returnedTask));
        }

        [Test]
        public void UpdateTaskTitleTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            var updatedTask = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project Y",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            taskRepository.Update(updatedTask);

            var retrievedTask = taskRepository.GetById(1);

            Assert.IsNotNull(retrievedTask);
            Assert.AreEqual(updatedTask.Id, retrievedTask.Id);
            Assert.AreEqual(updatedTask.Title, retrievedTask.Title);

            var departments = taskRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        [Test]
        public void UpdateTaskDescriptionTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            var updatedTask = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks forr Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            taskRepository.Update(updatedTask);

            var retrievedTask = taskRepository.GetById(1);

            Assert.IsNotNull(retrievedTask);
            Assert.AreEqual(updatedTask.Id, retrievedTask.Id);
            Assert.AreEqual(updatedTask.Description, retrievedTask.Description);

            var departments = taskRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        [Test]
        public void UpdateTaskDueDateTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            var updatedTask = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(8),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            taskRepository.Update(updatedTask);

            var retrievedTask = taskRepository.GetById(1);

            Assert.IsNotNull(retrievedTask);
            Assert.AreEqual(updatedTask.Id, retrievedTask.Id);
            Assert.AreEqual(updatedTask.DueDate, retrievedTask.DueDate);

            var departments = taskRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        [Test]
        public void UpdateTaskPriorityTesst()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            var updatedTask = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.Low,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            taskRepository.Update(updatedTask);

            var retrievedTask = taskRepository.GetById(1);

            Assert.IsNotNull(retrievedTask);
            Assert.AreEqual(updatedTask.Id, retrievedTask.Id);
            Assert.AreEqual(updatedTask.Priority, retrievedTask.Priority);

            var departments = taskRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        [Test]
        public void UpdateTaskStatusTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            var updatedTask = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Complete,
                AssigneeId = 1
            };

            taskRepository.Add(task);

            taskRepository.Update(updatedTask);

            var retrievedTask = taskRepository.GetById(1);

            Assert.IsNotNull(retrievedTask);
            Assert.AreEqual(updatedTask.Id, retrievedTask.Id);
            Assert.AreEqual(updatedTask.Status, retrievedTask.Status);

            var departments = taskRepository.GetAll();
            Assert.AreEqual(1, departments.Count);

        }

        /*[Test]
        public void FilterDepartmentTest() {
            var specificationMock = new Mock<Specification<Department>>();

            specificationMock.Setup(spec => spec.IsSatisfied(It.IsAny<Department>())).Returns((Department d) => d.Name == "It");

            var itDepartment = new Department { Id = 1, Name = "It" };
            var ManagementDepartment = new Department { Id = 2, Name = "Management" };
            departmentRepository.Add(itDepartment);
            departmentRepository.Add(ManagementDepartment);


            var departments =  departmentRepository.Filter(specificationMock.Object);

            Assert.That(departments.Count,Is.EqualTo(1));
        }*/


        [Test]
        public void GetNextTaskIdTest()
        {
            var id = taskRepository.GetNextId();
            Assert.That(id, Is.EqualTo(1));
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };
            taskRepository.Add(task);
            var nextId = taskRepository.GetNextId();
            Assert.That(nextId, Is.EqualTo(2));
        }

        [Test]
        public async Task SaveChangesAsyncTest()
        {
            var task = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };
            taskRepository.Add(task);

            await taskRepository.SaveChangesAsync();

            databaseManagerMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

    }
}
