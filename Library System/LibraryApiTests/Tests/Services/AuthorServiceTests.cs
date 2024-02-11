using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Xunit;

namespace LibraryApiTests.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly IAuthorService AuthorService;
        private readonly IBookService BookService;
        private readonly IUtilsTests Utils;

        public AuthorServiceTests()
        {
            this.BookService = new BookService(new RepositoryService<Book>());
            this.AuthorService = new AuthorService(new RepositoryService<Author>(), this.BookService);
            this.Utils = new UtilsTests();
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
                BithDate = DateTime.Now.AddYears(-30),
                Books = new List<Book>()
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
            Assert.Null(libraryAuthor.Books);

            // Update the author data
            author.Nationality = "Spanish";
            AuthorService.Update(author);

            // Get the author and check values
            libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Equal(author.Nationality, libraryAuthor.Nationality);

            // Take the author's books
            List<Book> librayAuthorBooks = libraryAuthor.Books;

            // Delete the author
            AuthorService.DeleteById(author.Id);

            // Check if the author doesn't exists
            Assert.Null(AuthorService.GetById(author.Id));
        }

        [Fact]
        public void CRUDAurhorWithBooksTest()
        {
            // Create an author with a book
            Author author = new Author()
            {
                Id = 2,
                Name = "Author test 2",
                Nationality = "Spanish",
                BithDate = DateTime.Now.AddYears(-30),
                Books = new List<Book>()
                {
                    new Book()
                    {
                        Id = 10,
                        Isbn = Utils.GenerateISBN(),
                        Name = "Book test 10",
                        PublicationDate = DateTime.Now
                    }
                }
            };

            AuthorService.Add(author);

            // Get the author and the book
            Author libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Single(libraryAuthor.Books);

            // Add another book
            author.Books.Add(new Book()
            {
                Id = 11,
                Isbn = Utils.GenerateISBN(),
                Name = "Book test 11",
                PublicationDate = DateTime.Now
            });

            AuthorService.Update(author);

            // Get the author and the book
            libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Equal(2, libraryAuthor.Books.Count());

            // Try to assign a no existing book to a no existing author
            Assert.Throws<InvalidOperationException>(() => AuthorService.AddExistingBookToAuthorById(3, 99));

            // Try to assign a no existing book to a existing author
            Assert.Throws<InvalidOperationException>(() => AuthorService.AddExistingBookToAuthorById(author.Id, 99));

            // Create a book and then assign to the author
            Book book = new Book()
            {
                Id = 12,
                Isbn = Utils.GenerateISBN(),
                Name = "Book test 12",
                PublicationDate = DateTime.Now
            };

            BookService.Add(book);
            AuthorService.AddExistingBookToAuthorById(author.Id, book.Id);

            // Get the author and validate the author haves the book
            libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Contains(book, libraryAuthor.Books);

            // Delete the author and check if the books still exists
            List<Book> deletedAuthorbooks = libraryAuthor.Books;

            AuthorService.DeleteById(author.Id);
            Assert.Null(AuthorService.GetById(author.Id));

            foreach (Book deletedAuthorBook in deletedAuthorbooks)
            {
                Assert.NotNull(BookService.GetById(deletedAuthorBook.Id));
            }

            // Create the same author again and assign the existing books
            author = new Author()
            {
                Id = 2,
                Name = "Author test 2",
                Nationality = "Spanish",
                BithDate = DateTime.Now.AddYears(-30),
                Books = deletedAuthorbooks
            };

            AuthorService.Add(author);
            libraryAuthor = AuthorService.GetById(author.Id);
            Assert.Equal(deletedAuthorbooks, libraryAuthor.Books);

            // Delete the author with the books
            AuthorService.DeleteWithAllDataById(author.Id);
            Assert.Null(AuthorService.GetById(author.Id));

            foreach (Book deletedBook in deletedAuthorbooks)
            {
                Assert.Null(BookService.GetById(deletedBook.Id));
            }
        }

        [Fact]
        public void AuthorsWithBooksTest()
        {
            // Create an author with a book
            Book book = new Book()
            {
                Id = 10,
                Isbn = Utils.GenerateISBN(),
                Name = "Book test 10",
                PublicationDate = DateTime.Now
            };

            Author author1 = new Author()
            {
                Id = 3,
                Name = "Author test 3",
                Nationality = "Spanish",
                BithDate = DateTime.Now.AddYears(-35),
                Books = new List<Book>()
                {
                    book
                }
            };

            AuthorService.Add(author1);

            // Create a second author with the same book, excepcting to throws an exception
            Author author2 = new Author()
            {
                Id = 4,
                Name = "Author test 4",
                Nationality = "English",
                BithDate = DateTime.Now.AddYears(-52),
                Books = new List<Book>()
                {
                    book
                }
            };

            Assert.Throws<InvalidOperationException>(() => AuthorService.Add(author2));

            // Assign again the book to the second author
            author2.Books.Add(book);

            // Try to update the author, expexting the same exception
            Assert.Throws<InvalidOperationException>(() => AuthorService.Update(author2));

            // Try to assign the book with another method
            AuthorService.AddExistingBookToAuthorById(author2.Id, book.Id);

            // Check if the author1 doesn't has the book
            Author authorLibrary = AuthorService.GetById(author1.Id);
            Assert.DoesNotContain(book, authorLibrary.Books);

            // Check if the author2 has the book
            authorLibrary = AuthorService.GetById(author2.Id);
            Assert.Contains(book, authorLibrary.Books);
        }
    }
}
