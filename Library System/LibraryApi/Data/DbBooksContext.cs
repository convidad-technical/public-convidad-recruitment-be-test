using LibraryDatabase.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDatabase.Data
{
    public class DbBooksContext : DbContext
    {
        public DbBooksContext(DbContextOptions<DbBooksContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public void InitializeMockData()
        {
            var Authors = new List<Author>
            {
                new Author { Id = 0, Name = "Stephen King", Nationality = "USA", BithDate = DateUtils.DateFromStringES("21/09/1947") },
                new Author { Id = 1, Name = "Caros Ruiz Zafón", Nationality = "Spain", BithDate = DateUtils.DateFromStringES("25/09/1964") },
                new Author { Id = 0, Name = "Patrick Rothfuss", Nationality = "USA", BithDate = DateUtils.DateFromStringES("06/06/1973") }
            };
            var Books = new List<Book>
            {
                new Book { Id = 0, Name = "It", Isbn = "9783453435773", PublicationDate = DateUtils.DateFromStringES("15/09/1986"), Author = Authors[0]},
                new Book { Id = 1, Name = "El resplandor", Isbn = "9783453435774", PublicationDate = DateUtils.DateFromStringES("28/01/1977"), Author = Authors[0]},

                new Book { Id = 2, Name = "Marina", Isbn = "9783453435775", PublicationDate = DateUtils.DateFromStringES("01/01/1999"), Author = Authors[1]},
                new Book { Id = 3, Name = "La sombra del viento", Isbn = "9783453435776", PublicationDate = DateUtils.DateFromStringES("01/01/2001"), Author = Authors[1]},

                new Book { Id = 4, Name = "El nombre del viento", Isbn = "9783453435777", PublicationDate = DateUtils.DateFromStringES("27/03/2007"), Author = Authors[2]},
                new Book { Id = 5, Name = "El temor de un hombre sabio", Isbn = "9783453435778", PublicationDate = DateUtils.DateFromStringES("01/03/2011"), Author = Authors[2]},
            };

            this.AddRange(Authors);
            this.AddRange(Books);
        }
    }
}
