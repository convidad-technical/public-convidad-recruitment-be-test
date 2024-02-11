using LibraryDatabase.Domain;

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
    }
}