﻿using Application.Models;
using Application.Ports;
using Application.Specifications;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories; 

public class FileReportingLineRepository : IRepository<ReportingLine>
{
    AbstractDatabaseManager _database;
    public FileReportingLineRepository(AbstractDatabaseManager database)
    {
        _database = database;
    }
    public void Add(ReportingLine entity)
    {
        _database.ReportingLines.Add(entity);
    }

    public List<ReportingLine> GetAll()
    {
        return _database.ReportingLines;
    }

    public ReportingLine? GetById(int? id)
    {
        return _database.ReportingLines.FirstOrDefault(t => t.Id == id);
    }

    public void Update(ReportingLine entity)
    {
        var old = _database.ReportingLines.FirstOrDefault(t => t.Id == entity.Id);
        _database.ReportingLines.Remove(old);
        _database.ReportingLines.Add(entity);
    }

    public List<ReportingLine> Filter(Specification<ReportingLine> specification)
    {
        return _database.ReportingLines.Where(t => specification.IsSatisfied(t)).ToList();
    }
    public int GetNextId()
    {
        return _database.ReportingLines.Count > 0 ? _database.ReportingLines.Max(x => x.Id) + 1 : 1;
    }

    public async Task SaveChangesAsync()
    {
        await _database.SaveChangesAsync();
    }
}

