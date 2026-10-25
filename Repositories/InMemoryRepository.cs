/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Collections.Generic;
using System.Linq;

public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
{
    private readonly Dictionary<string, T> _storage = new Dictionary<string, T>();

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (_storage.ContainsKey(item.Id))
        {
            throw new DuplicateIdException { DuplicateId = item.Id };
        }
        _storage[item.Id] = item;
    }

    public bool Remove(string id)
    {
        return _storage.Remove(id);
    }

    public T? FindById(string id)
    {
        return _storage.TryGetValue(id, out var item) ? item : default;
    }

    public IReadOnlyList<T> GetAll()
    {
        return _storage.Values.ToList().AsReadOnly();
    }

    public IReadOnlyList<T> FindAll(Predicate<T> condition)
    {
        return _storage.Values.Where(x => condition(x)).ToList().AsReadOnly();
    }
}
