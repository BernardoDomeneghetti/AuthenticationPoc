using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Models;
using AuthenticationPoc.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationWorker _authenticationWorker;
        private readonly UserDtoValidator _userDtoValidator;

        public AuthenticationController(IAuthenticationWorker authenticationWorker, UserDtoValidator userDtoValidator)
        {
            _authenticationWorker = authenticationWorker;
            _userDtoValidator = userDtoValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserDto userDto)
        {
            var validation = _userDtoValidator.Validate(userDto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            return Ok(await _authenticationWorker.RegisterNewUser(userDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var validation = _userDtoValidator.Validate(userDto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _authenticationWorker.Login(userDto);
            
            return result.Success? 
                Ok(result):BadRequest("Logou errado otário");
        }

        
    }
}
