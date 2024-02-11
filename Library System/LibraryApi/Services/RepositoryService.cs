using LibraryDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

public class RepositoryService<T> : IRepository<T> where T : IEntity
{
    private List<T> Data = new List<T>();

    public T GetById(int id)
    {
        return this.Data.FirstOrDefault(e => e.Id == id);
    }

    public List<T> GetAll()
    {
        return this.Data;
    }

    public T Add(T entity)
    {
        if (this.Data.Any(o => o.Id == entity.Id))
        {
            throw new ArgumentException($"Entity with Id {entity.Id} already exists.");
        }

        this.Data.Add(entity);
        return entity;
    }

    public void Update(T entity)
    {
        int index = this.Data.FindIndex(e => e.Id == entity.Id);
        if (index != -1)
        {
            this.Data[index] = entity;
        }
    }

    public void DeleteById(int id)
    {
        T entityToRemove = this.Data.FirstOrDefault(o => o.Id.Equals(id));

        if (entityToRemove != null)
        {
            this.Data.Remove(entityToRemove);
        }
    }
}