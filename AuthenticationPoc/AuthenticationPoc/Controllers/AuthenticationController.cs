using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationWorker _authenticationWorker;

        public AuthenticationController(IAuthenticationWorker authenticationWorker)
        {
            _authenticationWorker = authenticationWorker;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserDto userDto)
        {
            return Ok(await _authenticationWorker.RegisterNewUser(userDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserDto userDto)
        {
            var result = await _authenticationWorker.Login(userDto);
            
            return result.Success? 
                Ok(result):BadRequest("Logou errado otário");
        }

        
    }
}
