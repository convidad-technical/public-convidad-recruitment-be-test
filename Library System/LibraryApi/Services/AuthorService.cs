using LibraryDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> RepositoryService;
        private readonly IBookService BookService;

        public AuthorService(IRepository<Author> repositoryService, IBookService bookService)
        {
            this.RepositoryService = repositoryService;
            this.BookService = bookService;
        }

        Author IRepository<Author>.GetById(int id)
        {
            return this.RepositoryService.GetById(id);
        }

        List<Author> IRepository<Author>.GetAll()
        {
            return this.RepositoryService.GetAll();
        }

        Author IRepository<Author>.Add(Author author)
        {
            this.ValidateEntity(author);

            this.RepositoryService.Add(author);

            this.AddOrUpdateBooks(author.Id, author.Books);

            return author;
        }

        void IRepository<Author>.Update(Author author)
        {
            this.ValidateEntity(author);

            this.AddOrUpdateBooks(author.Id, author.Books);

            this.RepositoryService.Update(author);
        }

        void IRepository<Author>.DeleteById(int id)
        {
            this.RepositoryService.DeleteById(id);
        }

        public void DeleteWithAllDataById(int id)
        {
            Author author = this.RepositoryService.GetById(id);

            if (author != null)
            {
                if (author.Books != null && author.Books.Any())
                {
                    foreach (Book book in author.Books)
                    {
                        this.BookService.DeleteById(book.Id);
                    }
                }

                this.RepositoryService.DeleteById(author.Id);
            }
        }

        public void AddExistingBookToAuthorById(int idAuthor, int idBook)
        {
            Author author = this.RepositoryService.GetById(idAuthor);

            if (author == null)
            {
                throw new InvalidOperationException($"The author with id {idAuthor} doesnt't exists");
            }

            Book book = this.BookService.GetById(idBook);

            if (book == null) 
            {
                throw new InvalidOperationException($"The book with id {idBook} doesnt't exists");
            }

            if (!author.Books.Contains(book))
            {
                Author authorOfTheBook = this.GetAuthorByBookId(idBook); 
                
                if (authorOfTheBook != null)
                {
                    authorOfTheBook.Books.Remove(book);
                }

                author.Books.Add(book);
            }
        }

        public Author GetAuthorByBookId (int bookId)
        {
            Author author;
            Book book;

            List<Author> libraryAuthors = this.RepositoryService.GetAll();

            foreach (Author libraryAuthor in libraryAuthors)
            {
                book = libraryAuthor.Books.Where(o => o.Id == bookId).FirstOrDefault();

                if (book != null)
                {
                    return libraryAuthor;
                }
            }

            return null;
        }

        private void AddOrUpdateBooks(int authorId, List<Book> books)
        {
            Book book;
            Author author;
            foreach (var authorBook in books)
            {
                book = this.BookService.GetById(authorBook.Id);
                if (book == null)
                {
                    this.BookService.Add(authorBook);
                }
                else
                {
                    author = this.GetAuthorByBookId(authorBook.Id);

                    if (author != null && author.Id != authorId)
                    {
                        throw new InvalidOperationException($"The book with id {book.Id} is assigned to another author");
                    }

                    this.BookService.Update(authorBook);
                }
            }
        }

        private void ValidateEntity(Author author)
        {
            if (string.IsNullOrEmpty(author.Name))
            {
                throw new ArgumentNullException($"The name of the author cannot be empty or null.");
            }
        }
    }
}