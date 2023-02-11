using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeathCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("I'm alive");
        }
    }
}
