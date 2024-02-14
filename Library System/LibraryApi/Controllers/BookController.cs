using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;

namespace LibraryDatabase.Controllers
{
    [Route("api/books")]
    public class BookController : Controller
    {
        private readonly IBookService BookService;
        private readonly IAuthorService AuthorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            this.BookService = bookService;
            this.AuthorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAllBooks(
            [FromQuery] string name,
            [FromQuery] int year,
            [FromQuery] string authorName)
        {
            return Ok(this.BookService.GetAll(name, year, authorName));
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            Book book = this.BookService.GetById(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} not found.");
            }
            else
            {
                Author author = this.AuthorService.GetById(book.AuthorId);

                return Ok(new BookWithAuthorDTO()
                {
                    Book = book,
                    Author = author
                });
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                book = this.BookService.Add(book);
                return Ok(book.Id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}