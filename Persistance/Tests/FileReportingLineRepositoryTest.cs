using Application.Models;
using Application.Specifications.ReportingLineSpecification;
using Application.Specifications.UserSpecifications;
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
        public void GetAllReportingLinesTest()
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

        public void GetReportingLineByIdTest()
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

        [Test]
        public void FilterActiveReportingLineTest() {
            var firstReportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            var secondReportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 101,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
                EndDate= DateOnly.FromDateTime(new DateTime(2023,2,1))
            };

            reportingLineRepository.Add(firstReportingLine);
            reportingLineRepository.Add(secondReportingLine);
            var specification = new ActiveReportingLineSpecification();

            var filteredusers = reportingLineRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }
        [Test]
        public void FilterReportingLineManagerTest()
        {
            var firstReportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            var secondReportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 102,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
              
            };

            reportingLineRepository.Add(firstReportingLine);
            reportingLineRepository.Add(secondReportingLine);
            var specification = new ReportingLineManagerSpecification(100);

            var filteredusers = reportingLineRepository.Filter(specification);


            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }
        [Test]
        public void FilterReportingLineByUserAndManagerTest()
        {
            var firstReportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 100,
                SubordinateId = 201,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            var secondReportingLine = new ReportingLine
            {
                Id = 2,
                ManagerId = 102,
                SubordinateId = 200,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),

            };

            reportingLineRepository.Add(firstReportingLine);
            reportingLineRepository.Add(secondReportingLine);

           var  specification = new UserAndManagerReportingLineSpecification(100, 201);
           var filteredusers = reportingLineRepository.Filter(specification);
            Assert.That(filteredusers.Count, Is.EqualTo(1));
        }

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
