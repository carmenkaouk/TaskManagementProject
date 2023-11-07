using Application.Models;
using Application.Ports;
using Application.Services;
using Application.Specifications;
using Application.Specifications.ReportingLineSpecification;
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
    public class ReportingLineServiceTest
    {
        private Mock<IRepository<ReportingLine>> reportingLineRepositoryMock;
        private ReportingLineService reportingLineService;

        [SetUp]
        public void SetUp()
        {
            reportingLineRepositoryMock = new Mock<IRepository<ReportingLine>>();

            reportingLineService = new ReportingLineService(reportingLineRepositoryMock.Object);
        }

        [Test]
        public void AddReportingLineTest()
        {
            int userId = 100;
            int managerId = 50;

            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.GetNextId()).Returns(3);

            reportingLineService.AddReportingLine(userId, managerId);

            reportingLineRepositoryMock.Verify(repo => repo.Add(It.IsAny<ReportingLine>()), Times.Once);
            reportingLineRepositoryMock.Verify(repo => repo.GetNextId(), Times.Once);
        }

        [Test]
        public void GetReportingLineTest()
        {
            int managerId = 50;
            int userId = 100;

            var specification = new UserAndManagerReportingLineSpecification(50, 100);

            var reportingline1 = new ReportingLine
            {
                Id = 1,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };

            var reprtingLine2 = new ReportingLine
            {
                Id = 2,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
            };
            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.Filter(It.Is<UserAndManagerReportingLineSpecification>(spec => spec.GetType() == specification.GetType()))).Returns(new List<ReportingLine> {
                reportingline1,
                reprtingLine2,
            });

            var reportingLine = reportingLineService.GetReportingLine(managerId, userId);

            Assert.IsNotNull(reportingLine);

            Assert.That(reportingLine, Is.EqualTo(reportingline1));
        }


        [Test]
        public void EndReportingLineTest()
        {
            int managerId = 50;
            int userId = 100;

            var reportingLine = new ReportingLine
            {
                Id = 1,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1))
            };
            var specification = new UserAndManagerReportingLineSpecification(50, 100);

            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.Filter(It.Is<UserAndManagerReportingLineSpecification>(spec => spec.GetType() == specification.GetType()))).Returns(new List<ReportingLine> {
                reportingLine
            });

            reportingLineService.EndReportingLine(userId, managerId);


            Assert.That(reportingLine.EndDate, Is.Not.EqualTo(DateOnly.MaxValue));

        }


        [Test]
        public void GetAllActiveReportingLineTest()
        {
            var reportingLine1 = new ReportingLine
            {
                Id = 1,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1))
            };
            var reportingLine2 = new ReportingLine
            {
                Id = 2,
                ManagerId = 50,
                SubordinateId = 101,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
                EndDate = DateOnly.FromDateTime(new DateTime(2023, 2, 1))
            };


            var specification = new ActiveReportingLineSpecification();

            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.Filter(It.Is<ActiveReportingLineSpecification>(spec => spec.GetType() == specification.GetType()))).Returns(new List<ReportingLine> { reportingLine1 });

            var reportingLines = reportingLineService.GetAllActiveReporting();

            Assert.That(reportingLines.Count, Is.EqualTo(1));
            Assert.That(reportingLines[0], Is.EqualTo(reportingLine1));

        }

        [Test]
        public void GetAllManagersSubordinatesHistoryTest()
        {

            
            var reportingLine1 = new ReportingLine
            {
                Id = 1,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1))
            };
            var reportingLine2 = new ReportingLine
            {
                Id = 2,
                ManagerId = 51,
                SubordinateId = 101,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
                EndDate = DateOnly.FromDateTime(new DateTime(2023, 12, 1))
            };


            var specification = new ReportingLineManagerSpecification(50);

            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.Filter(It.Is<ReportingLineManagerSpecification>(spec => spec.GetType() == specification.GetType()))).Returns(new List<ReportingLine> { reportingLine1 });

            var reportingLines = reportingLineService.GetAllManagersSubordinatesHistory(50);

            Assert.That(reportingLines.Count, Is.EqualTo(1));
            Assert.That(reportingLines[0], Is.EqualTo(reportingLine1));

        }

        [Test]
        public void GetAllReportingLineTest()
        {
            var reportingLine1 = new ReportingLine
            {
                Id = 1,
                ManagerId = 50,
                SubordinateId = 100,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1))
            };
            var reportingLine2 = new ReportingLine
            {
                Id = 2,
                ManagerId = 50,
                SubordinateId = 101,
                StartDate = DateOnly.FromDateTime(new DateTime(2023, 1, 1)),
                EndDate = DateOnly.FromDateTime(new DateTime(2023, 2, 1))
            };

            reportingLineRepositoryMock.Setup(reportingLine => reportingLine.GetAll()).Returns(new List<ReportingLine> { reportingLine1, reportingLine2 });

            var reportingLines = reportingLineService.GetAllReportingLines();

            Assert.That(reportingLines.Count, Is.EqualTo(2));

        }
    }
}
