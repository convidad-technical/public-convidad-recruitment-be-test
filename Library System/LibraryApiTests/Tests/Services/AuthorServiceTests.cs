using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Xunit;

namespace LibraryApiTests.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly IAuthorService AuthorService;

        public AuthorServiceTests()
        {
            this.AuthorService = new AuthorService(new RepositoryService<Author>());
        }

        [Fact]
        public void CRUDAuthorTest()
        {
            // Create an author
            Author author = new Author()
            {
                Id = 1,
                Name = "Author test 1",
                Nationality = "",
                BithDate = DateTime.Now.AddYears(-30)
            };

            // Try to update the author, expecting throws an exception
            Assert.Throws<ArgumentException>(() => AuthorService.Update(author));

            // Add the author withot name expecting throws an exception
            author.Name = string.Empty;
            Assert.Throws<ArgumentNullException>(() => AuthorService.Add(author));

            // Sets the author's name and add it
            author.Name = "Author test 1";
            AuthorService.Add(author);

            // Get the author and check values
            Author libraryAuthor = AuthorService.GetById(author.Id);

            Assert.NotNull(libraryAuthor);
            Assert.Equal(author.Id, libraryAuthor.Id);
            Assert.Equal(author.Name, libraryAuthor.Name);
            Assert.Equal(author.Nationality, libraryAuthor.Nationality);
            Assert.Equal(author.BithDate, libraryAuthor.BithDate);

            // Update the author data
            author.Nationality = "Spanish";
            AuthorService.Update(author);

            // Get the author and check values
            libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Equal(author.Nationality, libraryAuthor.Nationality);

            // Delete the author
            AuthorService.DeleteById(author.Id);

            // Check if the author doesn't exists
            Assert.Null(AuthorService.GetById(author.Id));
        }
    }
}
