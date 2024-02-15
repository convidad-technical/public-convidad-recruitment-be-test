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

        List<Author> IRepository<Author>.GetAll(Func<Author, bool> predicate = null)
        {
            return this.RepositoryService.GetAll(predicate);
        }

        List<Author> IRepository<Author>.GetAllPaged(int page, int pageSize, Func<Author, bool> predicate = null)
        {
            return this.RepositoryService.GetAllPaged(page, pageSize, predicate);
        }

        Author IRepository<Author>.Add(Author author)
        {
            this.ValidateEntity(author);

            this.RepositoryService.Add(author);

            return author;
        }

        public List<int> GetAuthorIdsByAuthorName(string name)
        {
            return this.RepositoryService.GetAll(o => o.Name.Contains(name)).Select(o => o.Id).ToList();
        }

        private void ValidateEntity(Author author)
        {
            if (author.Id == 0)
            {
                throw new ArgumentNullException("The author id cannot be 0 or null.");
            }

            if (string.IsNullOrEmpty(author.Name))
            {
                throw new ArgumentNullException("The name of the author cannot be empty or null.");
            }
        }
    }
}