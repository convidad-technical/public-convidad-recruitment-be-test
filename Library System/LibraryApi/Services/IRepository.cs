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
    /// Retrieves all entries
    /// </summary>
    /// <returns></returns>
    List<T> GetAll();

    /// <summary>
    /// Retrieves all entries paged
    /// </summary>
    /// <returns></returns>
    List<T> GetAllPaged(int page, int pageSize);

    /// <summary>
    /// Adds a new entry
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    T Add(T entity);

    /// <summary>
    /// Updates an existing entry
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Update(T entity);

    /// <summary>
    /// Removes an entry by id
    /// </summary>
    /// <param name="id"></param>
    void DeleteById(int id);
}