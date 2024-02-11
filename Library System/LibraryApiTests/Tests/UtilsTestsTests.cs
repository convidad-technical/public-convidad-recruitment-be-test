using LibraryApiTests.Utils;
using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiTests.Tests
{
    public class UtilsTestsTests
    {
        private readonly IBookService BookService;
        private readonly IUtilsTests Utils;

        public UtilsTestsTests() 
        {
            RepositoryService<Book> repositoryService = new RepositoryService<Book>();
            this.BookService = new BookService(repositoryService);
            Utils = new UtilsTests();
        }

        [Fact]
        public void ISBNGeneratorTest()
        {
            string isbn = this.Utils.GenerateISBN();
            Assert.True(this.BookService.ValidateISBN(isbn));
        }
    }
}
