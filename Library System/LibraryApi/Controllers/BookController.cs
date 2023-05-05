using LibraryDatabase.Data;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private DbBooksContext context;
        public BookController(DbBooksContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(context.Books.ToList());
        }
        /*
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
        */
        [HttpPost]
        public IActionResult InitializeMockData()
        {
            this.context.InitializeMockData();
            return Ok();
        }

    }
}