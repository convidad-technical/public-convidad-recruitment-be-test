using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDatabase
{
    public class Author
    {

        #region Attributes

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Nationality { get; set; }

        public DateTime BithDate { get; set; }

        #endregion

        #region Relationships

        [NotMapped]
        public virtual ICollection<Book> Books { get; set; }

        #endregion
    }
}