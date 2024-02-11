using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService BookService;

        public BookController(IBookService bookService)
        {
            this.BookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(this.BookService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Getbook(int id)
        {
            Book book = this.BookService.GetById(id);
            if (book == null)
            {
                throw new Exception();
            }
            else
            {
                return Ok(book);
            }
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            book = this.BookService.Add(book);
            return Ok(book.Id);
        }
    }
}