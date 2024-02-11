using LibraryDatabase.Domain;

namespace LibraryDatabase.Controllers
{
    public class BookWithAuthorDTO
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}