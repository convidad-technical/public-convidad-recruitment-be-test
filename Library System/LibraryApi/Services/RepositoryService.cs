using System;
using System.Collections.Generic;
using System.Linq;

public class RepositoryService<T> : IRepository<T> where T : IEntity
{
    private readonly Dictionary<int, T> Data;

    public RepositoryService()
    {
        Data = new Dictionary<int, T>();
    }

    public T GetById(int id)
    {
        return this.Data.ContainsKey(id) ? this.Data[id] : default(T);
    }

    public List<T> GetAll(Func<T, bool> predicate = null)
    {
        if (predicate == null)
        {
            return this.Data.Values.ToList();
        }
        else
        {
            return this.Data.Values.Where(predicate).ToList();
        }
    }

    public List<T> GetAllPaged(int page, int pageSize, Func<T, bool> predicate = null)
    {
        if (predicate == null)
        {
            return this.Data.Values.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        else
        {
            return this.Data.Values.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }

    public T Add(T entity)
    {
        if (this.Data.ContainsKey(entity.Id))
        {
            throw new ArgumentException($"Entity with Id {entity.Id} already exists.");
        }

        this.Data.Add(entity.Id, entity);
        return entity;
    }
}