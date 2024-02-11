using LibraryApiTests.Utils;
using LibraryDatabase.Controllers;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryApiTests.Tests.Controllers
{
    public class ControllerTests
    {
        private readonly AuthorController AuthorController;
        private readonly BookController BookController;
        private readonly UtilsTests Utils;

        public ControllerTests()
        {
            AuthorService authorService = new AuthorService(new RepositoryService<Author>());
            BookService bookService = new BookService(new RepositoryService<Book>(), authorService);
            this.AuthorController = new AuthorController(authorService);
            this.BookController = new BookController(bookService, authorService);
            this.Utils = new UtilsTests();
        }

        [Fact]
        public void AddAuthorReturnsOKResult()
        {
            Author author = new Author()
            {
                Id = 20,
                Name = "Author test 20"
            };

            var result = this.AuthorController.AddAuthor(author) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(author.Id, result.Value);
        }

        [Fact]
        public void AddAuthorReturnsKOResult()
        {
            // Creates a new author, expecting an error
            Author author = new Author()
            {
                Id = 21,
                Name = ""
            };

            var result = this.AuthorController.AddAuthor(author) as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);

            // Sets the name and another id of the author, expecting an error again
            author = new Author()
            {
                Id = 0,
                Name = "Author test 21"
            };

            result = this.AuthorController.AddAuthor(author) as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);
        }

        [Fact]
        public void AddBookReturnsOKResult()
        {
            Author author = new Author
            {
                Id = 21,
                Name = "Author test 21"
            };

            this.AuthorController.AddAuthor(author);

            Book book = new Book()
            {
                Id = 20,
                Isbn = this.Utils.GenerateISBN(),
                Name = "Book test 20",
                PublicationDate = DateTime.Now,
                AuthorId = 21
            };

            var result = this.BookController.AddBook(book) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(book.Id, result.Value);
        }

        [Fact]
        public void AddBookReturnsKOResult()
        {
            // Create a new book and add it by API, expecting throws an exception
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
