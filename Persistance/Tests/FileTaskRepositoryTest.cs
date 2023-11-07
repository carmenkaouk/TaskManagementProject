using Application.Models;
using Application.Specifications.TaskToDoSpecification;
using Application.Specifications.UserSpecifications;
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
    //test
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

        [Test]
        public void FilterTasksByAssigneeTest() {
            var task1 = new TaskToDo
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
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 2
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            var specification = new TaskByAssigneeSpecification(1);

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterTasksByPriorityTest()
        {
            var task1 = new TaskToDo
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
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.Low,
                Status = Status.Pending,
                AssigneeId = 2
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            var specification = new TaskByPrioritySpecification(Priority.High);

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterTasksByStatusTest()
        {
            var task1 = new TaskToDo
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
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Complete,
                AssigneeId = 2
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            var specification = new TaskByStatusSpecification(Status.Pending);

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterTasksByUserTest()
        {
            var task1 = new TaskToDo
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
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 2
            };
            var task3 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 2,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            taskRepository.Add(task3);
            var specification = new TaskByUserSpecification(1);

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(2));
        }

        [Test]
        public void FilterTasksByDueDateTest()
        {
            var task1 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(9),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 2
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            var specification = new TaskDueBeforeDateSpecification(DateTime.Now.AddDays(8));

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterTasksAfterDateTest()
        {
            var task1 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(9),
                AssignmentDate = DateTime.Now,
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 1
            };
            var task2 = new TaskToDo
            {
                Id = 1,
                AssignedUserId = 1,
                Title = "Complete Project X",
                Description = "Finish all pending tasks for Project X",
                DueDate = DateTime.Now.AddDays(7),
                AssignmentDate = DateTime.Now.AddDays(3),
                Priority = Priority.High,
                Status = Status.Pending,
                AssigneeId = 2
            };

            taskRepository.Add(task1);
            taskRepository.Add(task2);
            var specification = new TasksAssignedAfterDateSpecification(DateTime.Now.AddDays(2));

            var filteredusers = taskRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

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
