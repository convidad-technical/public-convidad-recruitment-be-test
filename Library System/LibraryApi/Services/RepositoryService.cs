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

    public List<T> GetAll()
    {
        return this.Data.Values.ToList();
    }

    public List<T> GetAllPaged(int page, int pageSize)
    {
        return this.Data.Values.Skip(page).Take(pageSize).ToList();
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

    public void Update(T entity)
    {
        if (!this.Data.ContainsKey(entity.Id))
        {
            throw new ArgumentException($"Entity with Id {entity.Id} doesn't exists.");
        }

        this.Data[entity.Id] = entity;
    }

    public void DeleteById(int id)
    {
        if (this.Data.ContainsKey(id))
        {
            this.Data.Remove(id);
        }
    }
}