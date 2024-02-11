using LibraryDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Services
{
    public class BookService : IBookService, IRepository<Book>
    {
        private readonly IRepository<Book> RepositoryService;

        public BookService(IRepository<Book> repositoryService)
        {
            this.RepositoryService = repositoryService;
        }

        Book IRepository<Book>.GetById(int id)
        {
            return this.RepositoryService.GetById(id);
        }

        List<Book> IRepository<Book>.GetAll()
        {
            return this.RepositoryService.GetAll();
        }

        Book IRepository<Book>.Add(Book book)
        {
            this.ValidateEntity(book);

            return this.RepositoryService.Add(book);
        }

        void IRepository<Book>.Update(Book book)
        {
            this.RepositoryService.Update(book);
        }

        void IRepository<Book>.DeleteById(int id)
        {
            this.RepositoryService.DeleteById(id);
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
                throw new ArgumentNullException($"The name of the book cannot be empty or null.");
            }

            if (string.IsNullOrEmpty(book.Isbn))
            {
                throw new ArgumentNullException($"The ISBN of the book cannot be empty or null.");
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
                throw new ArgumentException($"The publication date of the book cannot be null.");
            }
        }
    }
}