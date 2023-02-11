using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestAuthorizationController : ControllerBase
    {
        [HttpGet("TestRoleAdmin")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> TestRoleAdmin()
        {
            return Ok("You are authorized as admin!");
        }

        [HttpGet("TestRoleSeller")]
        [Authorize(Roles = "Seller")]
        public ActionResult<string> TestRoleSeller()
        {
            return Ok("You are authorized as seller!");
        }

        [HttpGet("TestRoleUser")]
        [Authorize(Roles = "User")]
        public ActionResult<string> TestRoleUser()
        {
            return Ok("You are authorized as user!");
        }
    }
}
