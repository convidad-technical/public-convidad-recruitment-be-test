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

        public List<int> GetAuthorIdsByAuthorName(string name)
        {
            return this.RepositoryService.GetAll().Where(o => o.Name.Contains(name)).Select(o => o.Id).ToList();
        }

        public List<Author> GenerateRandomAuthors(int quantity)
        {
            Random random = new Random();
            List<Author> authors = new List<Author>();

            for (int i = 0; i < quantity; i++)
            {
                Author author = new Author
                {
                    Id = i + 1,
                    Name = "Author " + (i + 1),
                    Nationality = "Nationality " + (i + 1),
                    BirthDate = RandomDate(random)
                };
                authors.Add(author);
            }

            return authors;
        }

        private DateTime RandomDate(Random random)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
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