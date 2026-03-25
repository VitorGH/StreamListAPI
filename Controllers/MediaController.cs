using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MediaController(IHttpClientFactory http, IConfiguration cfg) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var client = http.CreateClient();
            var key = cfg["Tmdb:ApiKey"];
            var url = $"https://api.themoviedb.org/3/search/multi?api_key={key}&query={Uri.EscapeDataString(query)}&language=pt-BR";
            var response = await client.GetStringAsync(url);
            return Content(response, "application/json");
        }
    }
}
