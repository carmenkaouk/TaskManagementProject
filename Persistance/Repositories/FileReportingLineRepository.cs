using Application.Models;
using Application.Ports;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class FileReportingLineRepository : IRepository<ReportingLine>
    {
        AbstractDatabaseManager _database;
        public FileReportingLineRepository(AbstractDatabaseManager database)
        {
            _database = database;
        }
        public void Add(ReportingLine entity)
        {
            _database.ReportingLines.Add(entity);
            _database.SaveChangesAsync();
        }

        public List<ReportingLine> GetAll()
        {
            return _database.ReportingLines;
        }

        public ReportingLine GetById(int id)
        {
            return _database.ReportingLines.FirstOrDefault(t => t.Id == id);
        }

        public void Update(ReportingLine entity)
        {
            var old = _database.ReportingLines.FirstOrDefault(t => t.Id == entity.Id);
            _database.ReportingLines.Remove(old);
            _database.ReportingLines.Add(entity);
            _database.SaveChangesAsync();
        }

        //public List<ReportingLine> Filter(Specification<ReportingLine> specification)
        //{
        //    return _database.ReportingLines.Where(t => specification.IsSatisfied(t));
        //}
    }
}
