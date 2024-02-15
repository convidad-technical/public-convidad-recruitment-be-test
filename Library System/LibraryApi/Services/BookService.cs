using LibraryDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Services
{
    public class BookService : IBookService, IRepository<Book>
    {
        private readonly IRepository<Book> RepositoryService;
        private readonly IAuthorService AuthorService;

        public BookService(IRepository<Book> repositoryService, IAuthorService authorService)
        {
            this.RepositoryService = repositoryService;
            this.AuthorService = authorService;
        }

        Book IRepository<Book>.GetById(int id)
        {
            return this.RepositoryService.GetById(id);
        }

        List<Book> IRepository<Book>.GetAll(Func<Book, bool> predicate = null)
        {
            return this.RepositoryService.GetAll(predicate);
        }

        List<Book> IRepository<Book>.GetAllPaged(int page, int pageSize, Func<Book, bool> predicate = null)
        {
            return this.RepositoryService.GetAllPaged(page, pageSize, predicate);
        }

        Book IRepository<Book>.Add(Book book)
        {
            this.ValidateEntity(book);

            return this.RepositoryService.Add(book);
        }

        public bool ValidateISBN(string isbn)
        {
            // Remove hyphens and whitespaces from the ISBN
            isbn = isbn.Replace("-", "").Replace(" ", "");

            // Check if the ISBN has 13 digits
            if (isbn.Length != 13)
            {
                return false;
            }

            // Calculate the weighted sum of the digits of the ISBN
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            // Calculate the check digit
            int checkDigit = (10 - (sum % 10)) % 10;

            // Compare the check digit with the last digit of the ISBN
            return checkDigit == isbn[12] - '0';
        }

        public List<Book> GetAll(string name, int year, string authorName)
        {
            List<Book> list = this.RepositoryService.GetAll();
            List<int> authorIds = null;

            if (authorName != null)
            {
                authorIds = this.AuthorService.GetAuthorIdsByAuthorName(authorName);
            }

            list = list.Where(o =>
                (string.IsNullOrEmpty(name) || o.Name.Contains(name)) &&
                (year == 0 || o.PublicationDate.Year == year) &&
                (string.IsNullOrEmpty(authorName) || authorIds.Contains(o.AuthorId))
            ).ToList();


            return list;
        }

        private bool CheckIfISBNAlreadyExists(string isbn, int bookId)
        {
            List<Book> books = this.RepositoryService.GetAll();

            if (!books.Any())
            {
                return false;
            }

            return books.Any(o => o.Isbn == isbn && o.Id != bookId) ? true : false;
        }

        private void ValidateEntity(Book book)
        {
            if (string.IsNullOrEmpty(book.Name))
            {
                throw new ArgumentNullException("The name of the book cannot be empty or null.");
            }

            if (string.IsNullOrEmpty(book.Isbn))
            {
                throw new ArgumentNullException("The ISBN of the book cannot be empty or null.");
            }

            if (!this.ValidateISBN(book.Isbn))
            {
                throw new ArgumentException($"The following ISBN {book.Isbn} is incorrect.");
            }

            if (this.CheckIfISBNAlreadyExists(book.Isbn, book.Id))
            {
                throw new InvalidOperationException($"A book with the ISBN {book.Isbn} already exists.");
            }

            if (book.PublicationDate == null)
            {
                throw new ArgumentException("The publication date of the book cannot be null.");
            }

            if (book.AuthorId == 0)
            {
                throw new ArgumentNullException("The author id of the book cannot be 0 or null.");
            }

            if (this.AuthorService.GetById(book.AuthorId) == null)
            {
                throw new InvalidOperationException($"The author with id {book.AuthorId} doesn't exists.");
            }
        }
    }
}