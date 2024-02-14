using LibraryDatabase.Domain;
using LibraryDatabase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibraryDatabase.Controllers
{
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService AuthorService;
        private readonly IUtilsAuthor UtilsAuthor;
        private static bool AreAuthorsGenerated = false;

        public AuthorController(IAuthorService authorService, IUtilsAuthor utilsAuthor)
        {
            this.AuthorService = authorService;
            this.UtilsAuthor = utilsAuthor;
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
            if (size > 10000)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El tamaño máximo de página es 100000");
            }

            // Checks if the authors have not been generated
            if (!AreAuthorsGenerated)
            {
                this.UtilsAuthor.GenerateRandomAuthors(3000000);
                AreAuthorsGenerated = true;
            }

            int index = size * (page - 1);

            List<Author> authors = this.AuthorService.GetAllPaged(index, size);

            var json = JsonConvert.SerializeObject(authors, Formatting.Indented);

            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            return File(bytes, "application/json", "data.json");
        }
    }
}