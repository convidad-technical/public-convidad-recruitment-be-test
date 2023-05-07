using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryDatabase
{
    public class BooksFilters
    {
        public string Name { get; set; }

        public string Isbn { get; set; }

        public DateTime? PublicationDateAfter { get; set; }

        public DateTime? PublicationDateBefore { get; set; }

    }
}