using LibraryDatabase.Domain;
using System.Collections.Generic;

namespace LibraryDatabase.Services
{
    public interface IBookService : IRepository<Book>
    {
        /// <summary>
        /// Validates a 13-digit ISBN
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        bool ValidateISBN(string isbn);

        /// <summary>
        /// Gets all the books with filters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <param name="authorName"></param>
        /// <returns></returns>
        List<Book> GetAll(string name, int year, string authorName);
    }
}