using LibraryDatabase.Data;
using LibraryDatabase.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private DbBooksContext context;
        public BooksController(DbBooksContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetBooksFiltered([FromQuery] BooksFilters booksFilters)
        {
            IQueryable<Book> query = context.Books;

            if (booksFilters != null)
            {
                if (!string.IsNullOrEmpty(booksFilters.Name))
                {
                    query = query.Where(b => b.Name.Contains(booksFilters.Name));
                }
                if (!string.IsNullOrEmpty(booksFilters.Isbn))
                {
                    query = query.Where(b => b.Isbn.Contains(booksFilters.Isbn));
                }
                if (booksFilters.PublicationDateBefore.HasValue)
                {
                    query = query.Where(b => b.PublicationDate <= booksFilters.PublicationDateBefore);
                }
                if (booksFilters.PublicationDateAfter.HasValue)
                {
                    query = query.Where(b => b.PublicationDate >= booksFilters.PublicationDateAfter);
                }
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id, bool? getAuthor)
        {
            IQueryable<Book> query = context.Books;
            if (getAuthor.HasValue && getAuthor.Value)
            {
                query = query
                    .Include(b => b.Author);
            }

            var result = query.FirstOrDefault(b => b.Id == id);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult SaveBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (context.Authors.Any(a => a.Id == book.AuthorId))
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                    return Created(Request.Path, book);
                }
                else
                {
                    throw new Exception($"Could not find author with id: {book.AuthorId}");
                }
            }
            else
            {
                throw new Exception("Could not validate book");
            }
        }

        [HttpPost("initialize")]
        public IActionResult InitializeMockData()
        {
            context.InitializeMockData();
            return Ok();
        }
    }
}