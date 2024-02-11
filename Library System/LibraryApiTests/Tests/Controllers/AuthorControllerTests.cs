using LibraryDatabase.Controllers;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiTests.Tests.Controllers
{
    public class AuthorControllerTests
    {
        private readonly AuthorController AuthorController;

        public AuthorControllerTests()
        {
            BookService bookService = new BookService(new RepositoryService<Book>());
            AuthorService authorService = new AuthorService(new RepositoryService<Author>(), bookService);
            this.AuthorController = new AuthorController(authorService);
        }

        [Fact]
        public void AddAuthorReturnsOKResult()
        {
            Author author = new Author { Id = 10, Name = "Author test 10" };

            var result = this.AuthorController.AddAuthor(author) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(author.Id, result.Value);
        }

        [Fact]
        public void AddAuthorReturnsKOResult()
        {
            // Creates a new author
            Author author = new Author { Id = 11, Name = "" };

            // Add the author by API
            var result = this.AuthorController.AddAuthor(author) as ObjectResult;

            // Check the error
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);
        }
    }
}
