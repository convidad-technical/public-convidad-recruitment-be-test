using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService AuthorService;

        public AuthorController(IAuthorService authorService)
        {
            this.AuthorService = authorService;
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] Author author)
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

        [HttpGet]
        public IActionResult GetThreeMillionAuthors(
            [FromQuery] int page,
            [FromQuery] int size)
        {
            int startIndex = (page - 1) * size;

            List<Author> authors = this.AuthorService.GenerateRandomAuthors(3000000)
                                                        .Skip(startIndex)
                                                        .Take(size)
                                                        .ToList();

            return Ok(authors);
        }
    }
}