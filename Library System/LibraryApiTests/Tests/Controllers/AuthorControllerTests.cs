using LibraryDatabase.Controllers;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using LibraryDatabase.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryApiTests.Tests.Controllers
{
    public class AuthorControllerTests
    {
        private readonly AuthorController AuthorController;

        public AuthorControllerTests()
        {
            AuthorService authorService = new AuthorService(new RepositoryService<Author>());
            UtilsAuthor utilsAuthor = new UtilsAuthor(authorService);
            this.AuthorController = new AuthorController(authorService, utilsAuthor);
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
            // Creates an author, expecting an error because the name is empty
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
        public void GetThreeMillionAuthorsOK()
        {
            // Calls the method
            var result = this.AuthorController.GetThreeMillionAuthors(0, 500) as ActionResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetThreeMillionAuthorsKO()
        {
            // Calls the method with a negative number page, expecting throws an exception
            var result = this.AuthorController.GetThreeMillionAuthors(-1, 100) as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);

            // Calls the method with exceding the size page, expecting throws an exception
            result = this.AuthorController.GetThreeMillionAuthors(1, 20000) as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);
        }
    }
}
