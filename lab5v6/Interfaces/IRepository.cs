using System.Collections.Generic;

namespace Lab5.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
