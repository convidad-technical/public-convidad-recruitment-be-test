using LibraryDatabase.Domain;
using System.Collections.Generic;

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

            return this.RepositoryService.Add(author);
        }

        void IRepository<Author>.Update(Author author)
        {
            this.RepositoryService.Update(author);
        }

        void IRepository<Author>.DeleteById(int id)
        {
            this.RepositoryService.DeleteById(id);
        }

        private void ValidateEntity(Author author)
        {

        }
    }
}