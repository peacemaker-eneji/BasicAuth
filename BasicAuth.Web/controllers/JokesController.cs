using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;


namespace BasicAuth.Web.Controllers {

    [Route("api/jokes")]
    [ApiController]
    public class JokesController : ControllerBase {
        private readonly String _dadJokeUrl = "https://api.api-ninjas.com/v1/dadjokes";

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() {
            try {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Api-Key", Environment.GetEnvironmentVariable("X-Api-Key")!);
                using HttpResponseMessage res = await client.PostAsync(_dadJokeUrl, null);
                using HttpContent content = res.Content;
                var data = JObject.Parse(await content.ReadAsStringAsync());
                if (data is null) throw new Exception();
                return Ok(data[0][0]);
            } catch {
                return StatusCode(500, new { Error = "Something Went Wrong" });
            }
        }
    }
}
