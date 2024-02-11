using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryDatabase.Controllers
{
    [Route("api/author")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService AuthorService;

        public AuthorController(IAuthorService authorService)
        {
            this.AuthorService = authorService;
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            try
            {
                author = this.AuthorService.Add(author);
                return Ok(author.Id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}