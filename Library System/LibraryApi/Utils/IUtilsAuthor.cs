using LibraryDatabase.Domain;
using System.Collections.Generic;

namespace LibraryDatabase.Services
{
    public interface IUtilsAuthor
    {
        /// <summary>
        /// Generates random authors
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        List<Author> GenerateRandomAuthors(int quantity);
    }
}