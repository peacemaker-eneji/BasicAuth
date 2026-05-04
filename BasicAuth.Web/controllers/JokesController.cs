using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;


namespace BasicAuth.Web.Controllers {

    [ApiController]
    [Route("jokes")]
    public class JokesController : ControllerBase {
        private readonly String _dadJokeUrl = "https://api.api-ninjas.com/v1/dadjokes";

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Api-Key", Environment.GetEnvironmentVariable("X-Api-Key")!);
                using HttpResponseMessage res = await client.GetAsync(_dadJokeUrl);
                string content = await res.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(content);
                return Ok(data.RootElement[0]);
            } catch {
                return StatusCode(500, new { Error = "Something Went Wrong" });
            }
        }
    }
}
