using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using LibraryDatabase.Utils;
using Xunit;

namespace LibraryApiTests.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly IAuthorService AuthorService;
        private readonly IUtilsAuthor UtilsAuthor;

        public AuthorServiceTests()
        {
            this.AuthorService = new AuthorService(new RepositoryService<Author>());
            this.UtilsAuthor = new UtilsAuthor(this.AuthorService);
        }

        [Fact]
        public void NewAuthorTest()
        {
            // Creates an author
            Author author = new Author()
            {
                Id = 1,
                Name = "Author test 1",
                Nationality = "",
                BirthDate = DateTime.Now.AddYears(-30)
            };

            // Add the author withot name expecting throws an exception
            author.Name = string.Empty;
            Assert.Throws<ArgumentNullException>(() => AuthorService.Add(author));

            // Sets the author's name and add it
            author.Name = "Author test 1";
            AuthorService.Add(author);

            // Gets the author and check values
            Author libraryAuthor = AuthorService.GetById(author.Id);

            Assert.NotNull(libraryAuthor);
            Assert.Equal(author.Id, libraryAuthor.Id);
            Assert.Equal(author.Name, libraryAuthor.Name);
            Assert.Equal(author.Nationality, libraryAuthor.Nationality);
            Assert.Equal(author.BirthDate, libraryAuthor.BirthDate);
        }

        [Fact]
        public void GetAuthorIdsByNameTest()
        {
            // Creates an author
            Author author = new Author()
            {
                Id = 2,
                Name = "Author test 2 Paula",
                Nationality = "Spanish",
                BirthDate = DateTime.Now.AddYears(-32)
            };

            this.AuthorService.Add(author);

            // Search the author and compare
            int authorId = this.AuthorService.GetAuthorIdsByAuthorName("Paula").Single();
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public void GetAuthorsRandomPagedTest()
        {
            this.UtilsAuthor.GenerateRandomAuthors(20);
            List<Author> authors = this.AuthorService.GetAll();
            Assert.True(authors.Count >= 20);

            authors = this.AuthorService.GetAllPaged(0, 5);
            Assert.True(authors.Count == 5);

            authors = this.AuthorService.GetAllPaged(1, 5);
            Assert.True(authors.Count == 5);
        }
    }
}
