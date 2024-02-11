using LibraryDatabase.Domain;

namespace LibraryDatabase.Services
{
    public interface IAuthorService : IRepository<Author>
    {
        /// <summary>
        /// Deletes an author and the data associated with
        /// </summary>
        /// <param name="id"></param>
        void DeleteWithAllDataById(int id);

        /// <summary>
        /// Create a relation with an existing book and an author
        /// </summary>
        /// <param name="idAuthor"></param>
        /// <param name="idBook"></param>
        void AddExistingBookToAuthorById(int idAuthor, int idBook);

        /// <summary>
        /// Gets the author of a given book id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Author GetAuthorByBookId(int bookId);
    }
}