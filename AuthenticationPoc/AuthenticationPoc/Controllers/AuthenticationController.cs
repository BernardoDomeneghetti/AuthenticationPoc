using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserDto userDto)
        {

        }
    }
}
