using System;
using System.Collections.Generic;

public interface IRepository<T>
{
    /// <summary>
    /// Retrieves the entry with the specified id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T GetById(int id);

    /// <summary>
    /// Retrieves all entries with an optional filter
    /// </summary>
    /// <returns></returns>
    List<T> GetAll(Func<T, bool> predicate = null);

    /// <summary>
    /// Retrieves all entries paged with an optional filter
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    List<T> GetAllPaged(int page, int pageSize, Func<T, bool> predicate = null);

    /// <summary>
    /// Adds a new entry
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    T Add(T entity);
}