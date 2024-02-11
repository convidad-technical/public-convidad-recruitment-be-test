using System;
using System.Collections.Generic;

namespace LibraryDatabase.Domain
{
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime BithDate { get; set; }
        public List<Book> Books { get; set; }
    }
}