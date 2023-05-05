using System;

namespace LibraryDatabase
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}