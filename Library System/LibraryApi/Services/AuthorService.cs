using LibraryDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> RepositoryService;

        public AuthorService(IRepository<Author> repositoryService)
        {
            this.RepositoryService = repositoryService;
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

            return author;
        }

        void IRepository<Author>.Update(Author author)
        {
            this.ValidateEntity(author);

            this.RepositoryService.Update(author);
        }

        void IRepository<Author>.DeleteById(int id)
        {
            this.RepositoryService.DeleteById(id);
        }

        public Author GetAuthorByBookId(int bookId)
        {
            // TODO
            return new Author();
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