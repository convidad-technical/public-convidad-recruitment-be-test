using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Xunit;

namespace LibraryApiTests.Tests
{
    public class AuthorServiceTests
    {
        private readonly IBookService AuthorService;
        private readonly IUtilsTests Utils;

        public AuthorServiceTests()
        {
            RepositoryService<Book> repositoryService = new RepositoryService<Book>();
            this.AuthorService = new BookService(repositoryService);
            this.Utils = new UtilsTests();
        }
    }
}
