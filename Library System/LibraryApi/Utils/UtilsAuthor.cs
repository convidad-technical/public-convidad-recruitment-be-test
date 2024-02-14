using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Utils
{
    public class UtilsAuthor : IUtilsAuthor
    {
        private readonly IAuthorService AuthorService;

        public UtilsAuthor(IAuthorService authorService)
        {
            this.AuthorService = authorService;
        }

        public List<Author> GenerateRandomAuthors(int quantity)
        {
            Random random = new Random();
            List<Author> authors = new List<Author>();

            for (int i = 0; i < quantity; i++)
            {
                try
                {
                    Author author = new Author
                    {
                        Id = i + 1,
                        Name = "Author " + (i + 1),
                        Nationality = "Nationality " + (i + 1),
                        BirthDate = this.GenerateRandomDate(random)
                    };

                    this.AuthorService.Add(author);
                }
                catch (Exception)
                {
                    quantity++;
                }
            }

            return authors;
        }

        private DateTime GenerateRandomDate(Random random)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}