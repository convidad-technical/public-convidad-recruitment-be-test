using LibraryDatabase.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private DbBooksContext context;
        public AuthorsController(DbBooksContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(context.Authors.ToList());
        }

        [HttpPost]
        public IActionResult SaveAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return Created(Request.Path, author);
            }
            else
            {
                throw new Exception("Could not validate author");
            }
        }
    }
}