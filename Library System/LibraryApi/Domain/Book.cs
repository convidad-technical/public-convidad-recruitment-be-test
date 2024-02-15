using System;

namespace LibraryDatabase.Domain
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
    }
}