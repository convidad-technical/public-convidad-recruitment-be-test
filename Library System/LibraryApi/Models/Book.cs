using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDatabase
{
    public class Book
    {
        #region Attributes

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Isbn { get; set; }

        public DateTime PublicationDate { get; set; }

        public int AuthorId { get; set; }

        #endregion

        #region Relationships

        public Author Author { get; set; }

        #endregion

    }
}