using Application.Models;
using Application.Ports;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories; 

internal class FileTaskRepository : IRepository<TaskToDo>
{
    AbstractDatabaseManager _database; 
    public FileTaskRepository(AbstractDatabaseManager database)
    { 
        _database = database;
    }
    public void Add(TaskToDo entity)
    {
        _database.Tasks.Add(entity);
        _database.SaveChangesAsync();
    }

    public List<TaskToDo> GetAll()
    {
        return _database.Tasks;
    }

    public TaskToDo GetById(int id)
    {
        return _database.Tasks.FirstOrDefault(t => t.Id.Equals(id));
    }

    public void Update(TaskToDo entity)
    {
        var old = _database.Tasks.FirstOrDefault(t => t.Id.Equals(entity.Id));
        _database.Tasks.Remove(old);
        _database.Tasks.Add(entity);
        _database.SaveChangesAsync();
    }
    //public List<TaskToDo> Filter(Specification<TaskToDo> specification)
    //{
    //    return _database.Users.Where(t => specification.IsSatisfied(t));
    //}


}
