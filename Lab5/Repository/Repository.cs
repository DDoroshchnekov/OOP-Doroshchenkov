using System.Collections.Generic;
using System.Linq;
using Lab5.Interfaces;
using Lab5.Exceptions;

namespace Lab5.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _items = new();
        private readonly Func<T, int> _getId;

        public Repository(Func<T, int> getId)
        {
            _getId = getId;
        }

        public void Add(T item)
        {
            if (item == null)
                throw new InvalidItemException("Неможливо додати порожній елемент!");

            _items.Add(item);
        }

        public void Remove(int id)
        {
            var item = _items.FirstOrDefault(i => _getId(i) == id);
            if (item == null)
                throw new NotFoundException($"Елемент з ID {id} не знайдено!");
            _items.Remove(item);
        }

        public IEnumerable<T> GetAll() => _items;

        public T GetById(int id)
        {
            var item = _items.FirstOrDefault(i => _getId(i) == id);
            if (item == null)
                throw new NotFoundException($"Елемент з ID {id} не знайдено!");
            return item;
        }
    }
}
