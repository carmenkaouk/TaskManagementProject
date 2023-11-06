using Application.Models;
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
    public  class FileReportingLineRepositoryTest
    {
        private Mock<AbstractDatabaseManager> databaseManagerMock;
        private FileReportingLineRepository reportingLineRepository;

        [SetUp]
        public void SetUp()
        {
            databaseManagerMock = new Mock<AbstractDatabaseManager>();
            databaseManagerMock.Object.ReportingLines = new List<ReportingLine>();
            reportingLineRepository = new FileReportingLineRepository(databaseManagerMock.Object);
        }

        [Test]
        public void AddReportingLineTest()
        {
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);

            Assert.Contains(reportingLine, databaseManagerMock.Object.ReportingLines);
        }

        [Test]
        public void GetAllDepartmentsTest()
        {
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);

            var reportingLines = reportingLineRepository.GetAll();
            Assert.That(reportingLines.Count, Is.EqualTo(1));
        }

        [Test]

        public void GetDepartmentByIdTest()
        {
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);

            var returnedReportingLine = reportingLineRepository.GetById(1);
            Assert.That(reportingLine, Is.EqualTo(returnedReportingLine));
        }

        [Test]
        public void UpdateDepartmentTest()
        {
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            var updatedReportingLine= new ReportingLine
            {
                Id = 1,
                ManagerId = 101,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);

            reportingLineRepository.Update(updatedReportingLine);

            var retrievedReportingLine = reportingLineRepository.GetById(1);

            Assert.IsNotNull(retrievedReportingLine);
            Assert.AreEqual(updatedReportingLine.Id, retrievedReportingLine.Id);
            Assert.AreEqual(updatedReportingLine.ManagerId, retrievedReportingLine.ManagerId);
            Assert.AreEqual(updatedReportingLine.SubordinateId, retrievedReportingLine.SubordinateId);


            var reportingLines = reportingLineRepository.GetAll();
            Assert.AreEqual(1, reportingLines.Count);

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
        public void GetNextDepartmentIdTest()
        {
            var id = reportingLineRepository.GetNextId();
            Assert.That(id, Is.EqualTo(1));
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);
            var nextId = reportingLineRepository.GetNextId();
            Assert.That(nextId, Is.EqualTo(2));
        }

        [Test]
        public async Task SaveChangesAsyncTest()
        {
            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            reportingLineRepository.Add(reportingLine);

            await reportingLineRepository.SaveChangesAsync();

            databaseManagerMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
