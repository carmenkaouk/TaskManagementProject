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

public  class FileUserRepository : IRepository<User>
{
    AbstractDatabaseManager _database;

    public FileUserRepository(AbstractDatabaseManager database)
    {
        _database = database;
    }
    public void Add(User entity)
    {
        _database.Users.Add(entity);
    }

    public List<User> GetAll()
    {
        return _database.Users;
    }

    public User? GetById(int? id)
    {
        return _database.Users.FirstOrDefault(t => t.Id == id);
    }

    public void Update(User entity)
    {
        var old = _database.Users.FirstOrDefault(t => t.Id == entity.Id);
        _database.Users.Remove(old);
        _database.Users.Add(entity);
    }
    public List<User> Filter(Specification<User> specification)
    {
        return _database.Users.Where(t => specification.IsSatisfied(t)).ToList();

    }
    public int GetNextId()
    {
        return _database.Users.Count > 0 ? _database.Users.Max(x => x.Id) + 1 : 1;
    }

    public async Task SaveChangesAsync()
    {
        await _database.SaveChangesAsync();
    }
}
