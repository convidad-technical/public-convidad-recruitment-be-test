using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryDatabase.Controllers
{
    [Route("api/realbooks")]
    public class RealBookController : Controller
    {
        private readonly IHttpClientFactory ClientFactory;

        public RealBookController(IHttpClientFactory clientFactory)
        {
            this.ClientFactory = clientFactory;
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetRealBook(string isbn)
        {
            try
            {
                var client = this.ClientFactory.CreateClient();
                var response = await client.GetAsync($"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&jscmd=details&format=json");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error retrieving data from the Open Library API");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing the request: {e.Message}");
            }
        }
    }
}