using Application.Models;
using Application.Ports;
using Application.Specifications;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories; 

public class FileDepartmentRepository : IRepository<Department>
{
    AbstractDatabaseManager _database;
    public FileDepartmentRepository(AbstractDatabaseManager database)
    {
        _database = database;
    }
    public void Add(Department entity)
    {
        _database.Departments.Add(entity);
    }

    public List<Department> GetAll()
    {
        return _database.Departments;
    }

    public Department? GetById(int? id)
    {
        return _database.Departments.FirstOrDefault(t => t.Id == id);
    }

    public void Update(Department entity)
    {
        var old = _database.Departments.FirstOrDefault(t => t.Id == entity.Id);
        _database.Departments.Remove(old);
        _database.Departments.Add(entity);
    }

    public List<Department> Filter(Specification<Department> specification)
    {
        return _database.Departments.Where(t => specification.IsSatisfied(t)).ToList();
    }

    public int GetNextId()
    {
        return _database.Departments.Count > 0 ? _database.Departments.Max(x => x.Id) + 1 : 1;
    }

    public async Task SaveChangesAsync()
    {
        await _database.SaveChangesAsync();
    }
}
