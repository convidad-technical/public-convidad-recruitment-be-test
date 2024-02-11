using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Xunit;

namespace LibraryApiTests.Tests
{
    public class BookServiceTests
    {
        private readonly IBookService BookService;
        private readonly IUtilsTests Utils;

        public BookServiceTests()
        {
            RepositoryService<Book> repositoryService = new RepositoryService<Book>();
            this.BookService = new BookService(repositoryService);
            this.Utils = new UtilsTests();
        }

        [Fact]
        public void ISBNTest()
        {
            // ISBN must have 13 digits
            string isbn = "0";
            Assert.False(this.BookService.ValidateISBN(isbn));

            // Incorrect ISBN
            isbn = "978-3-16-148410-2";
            Assert.False(this.BookService.ValidateISBN(isbn));

            // Correct ISBN
            isbn = "978-3-16-148410-0";
            Assert.True(this.BookService.ValidateISBN(isbn));
        }

        [Fact]
        public void CRUDBookTest()
        {
            // Create a new book with ISBN and Name empty
            Book book = new Book()
            {
                Id = 1,
                Isbn = "",
                Name = "",
                PublicationDate = DateTime.Now
            };

            // Try to update the book, expecting throws an exception
            Assert.Throws<ArgumentException>(() => this.BookService.Update(book));

            // Add the book expecting throws an exception
            Assert.Throws<ArgumentNullException>(() => this.BookService.Add(book));

            // Sets the name of the book
            book.Name = "Book test 1";

            // Add the book expecting throws an exception
            Assert.Throws<ArgumentNullException>(() => this.BookService.Add(book));

            // Sets the ISBN of the book with an incorrect one
            book.Isbn = "0";

            // Add the book expecting throws an exception
            Assert.Throws<ArgumentException>(() => this.BookService.Add(book));

            // Sets a correct ISBN to save the book
            book.Isbn = this.Utils.GenerateISBN();
            this.BookService.Add(book);

            // Get the book and check values
            Book libraryBook = this.BookService.GetById(book.Id);

            Assert.NotNull(libraryBook);
            Assert.Equal(book.Id, libraryBook.Id);
            Assert.Equal(book.Isbn, libraryBook.Isbn);
            Assert.Equal(book.Name, libraryBook.Name);
            Assert.Equal(book.PublicationDate, libraryBook.PublicationDate);

            // Update the book data
            book.Isbn = this.Utils.GenerateISBN();
            book.Name = "Book test 1 updated";
            book.PublicationDate = DateTime.Now.AddMonths(-1);

            this.BookService.Update(book);

            // Get the book and check values
            libraryBook = this.BookService.GetById(book.Id);

            Assert.NotNull(libraryBook);
            Assert.Equal(book.Id, libraryBook.Id);
            Assert.Equal(book.Isbn, libraryBook.Isbn);
            Assert.Equal(book.Name, libraryBook.Name);
            Assert.Equal(book.PublicationDate, libraryBook.PublicationDate);

            // Delete the book
            this.BookService.DeleteById(book.Id);

            // Check if the book doesn't exists
            Assert.Null(this.BookService.GetById(libraryBook.Id));
        }

        [Fact]
        public void BookISBNAndIdExistsTest()
        {
            string isbn = this.Utils.GenerateISBN();

            // Create one book
            Book book = new Book()
            {
                Id = 2,
                Isbn = isbn,
                Name = "Book test 1",
                PublicationDate = DateTime.Now
            };

            this.BookService.Add(book);

            // Create a second book with the same Id
            book = new Book()
            {
                Id = 2,
                Isbn = isbn,
                Name = "Book test 2",
                PublicationDate = DateTime.Now
            };

            // Add the book expecting throws an exception
            Assert.Throws<ArgumentException>(() => this.BookService.Add(book));

            // Change the Id of the second book, expecting throws an exception because the repeated ISBN
            book.Id = 3;
            Assert.Throws<InvalidOperationException>(() => this.BookService.Add(book));

            // Change the ISBN of the second book
            book.Isbn = this.Utils.GenerateISBN();
            this.BookService.Add(book);

            // Check the 2 books exists
            Assert.NotNull(this.BookService.GetById(2));
            Assert.NotNull(this.BookService.GetById(3));
            Assert.Equal(2, this.BookService.GetAll().Count());
        }
    }
}