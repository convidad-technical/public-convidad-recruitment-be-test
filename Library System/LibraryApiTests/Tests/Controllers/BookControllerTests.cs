using LibraryApiTests.Utils;
using LibraryDatabase.Controllers;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using LibraryDatabase.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryApiTests.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly AuthorController AuthorController;
        private readonly BookController BookController;
        private readonly UtilsTests Utils;
        private readonly IAuthorService AuthorService;
        private readonly IBookService BookService;

        public BookControllerTests()
        {
            this.AuthorService = new AuthorService(new RepositoryService<Author>());
            this.BookService = new BookService(new RepositoryService<Book>(), this.AuthorService);
            UtilsAuthor utilsAuthor = new UtilsAuthor(this.AuthorService);
            this.AuthorController = new AuthorController(this.AuthorService, utilsAuthor);
            this.BookController = new BookController(this.BookService, this.AuthorService);
            this.Utils = new UtilsTests();
        }

        [Fact]
        public void AddBookReturnsOKResult()
        {
            // Creates an author
            Author author = new Author
            {
                Id = 21,
                Name = "Author test 21"
            };

            this.AuthorController.AddAuthor(author);

            // Creates a book
            Book book = new Book()
            {
                Id = 20,
                Isbn = this.Utils.GenerateISBN(),
                Name = "Book test 20",
                PublicationDate = DateTime.Now,
                AuthorId = 21
            };

            var result = this.BookController.AddBook(book) as OkObjectResult;

            // Validates response and data
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(book.Id, result.Value);
        }

        [Fact]
        public void AddBookReturnsKOResult()
        {
            // Creates a book of an author who doesn't exists, expecting an error
            Book book = new Book()
            {
                Id = 21,
                Isbn = this.Utils.GenerateISBN(),
                Name = "Book test 21",
                PublicationDate = DateTime.Now,
                AuthorId = 22
            };

            var result = this.BookController.AddBook(book) as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);
        }
    }
}
