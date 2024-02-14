using LibraryDatabase.Domain;
using System.Collections.Generic;

namespace LibraryDatabase.Services
{
    public interface IAuthorService : IRepository<Author>
    {
        /// <summary>
        /// Get a list of author Ids by a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<int> GetAuthorIdsByAuthorName(string name);
    }
}