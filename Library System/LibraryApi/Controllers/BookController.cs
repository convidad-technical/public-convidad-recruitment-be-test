using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        public Dictionary<int, Book> Librarybooks = new Dictionary<int, Book>();
        public Dictionary<int, Author> Libraryauthors = new Dictionary<int, Author>();

        public BookController(Dictionary<int, Book> librarybooks)
        {
            this.Librarybooks = librarybooks;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(Librarybooks.Values);
        }

        [HttpGet("{id}")]
        public IActionResult getbook(int id)
        {
            if (!Librarybooks.ContainsKey(id))
            {
                throw new Exception();
            }
            else
            {
                return Ok(Librarybooks[id]);
            }
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            Librarybooks.Add(book.Id, book);
            return Ok(book.Id);
        }
    }
}