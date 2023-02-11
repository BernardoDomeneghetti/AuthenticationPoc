using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Models;
using AuthenticationPoc.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationWorker _authenticationWorker;
        private readonly LoginDtoValidator _loginDtoValidator;
        private readonly RegisterDtoValidator _registerDtoValidator;

        public AuthenticationController(IAuthenticationWorker authenticationWorker, LoginDtoValidator userDtoValidator, RegisterDtoValidator registerDtoValidator)
        {
            _authenticationWorker = authenticationWorker;
            _loginDtoValidator = userDtoValidator;
            _registerDtoValidator = registerDtoValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (RegisterDto registerDto)
        {
            var validation = _registerDtoValidator.Validate(registerDto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            await _authenticationWorker.RegisterNewUser(registerDto);
            
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var validation = _loginDtoValidator.Validate(userDto);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = await _authenticationWorker.Login(userDto);
            
            return result.Success? 
                Ok(result):BadRequest("Logou errado otário");
        }

        
    }
}
