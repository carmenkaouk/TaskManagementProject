using Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? GetById(int? id);
        void Add(T entity);
        void Update(T entity);
        List<T> Filter(Specification<T> specification);
        int GetNextId();
        Task SaveChangesAsync();

    }
}
