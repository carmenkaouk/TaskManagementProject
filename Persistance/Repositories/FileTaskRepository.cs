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

public class FileTaskRepository : IRepository<TaskToDo>
{
    AbstractDatabaseManager _database; 
    public FileTaskRepository(AbstractDatabaseManager database)
    { 
        _database = database;
    }
    public void Add(TaskToDo entity)
    {
        _database.Tasks.Add(entity);
    }

    public List<TaskToDo> GetAll()
    {
        return _database.Tasks;
    }

    public TaskToDo? GetById(int? id)
    {
        return _database.Tasks.FirstOrDefault(t => t.Id.Equals(id));
    }

    public void Update(TaskToDo entity)
    {
        var old = _database.Tasks.FirstOrDefault(t => t.Id.Equals(entity.Id));
        _database.Tasks.Remove(old);
        _database.Tasks.Add(entity);
    }
    public List<TaskToDo> Filter(Specification<TaskToDo> specification)
    {
        return _database.Tasks.Where(t => specification.IsSatisfied(t)).ToList();
    }

    public int GetNextId()
    {
        return _database.Tasks.Count > 0 ? _database.Tasks.Max(x => x.Id) + 1 : 1;
    }

    public async Task SaveChangesAsync()
    {
        await _database.SaveChangesAsync();
    }
}
