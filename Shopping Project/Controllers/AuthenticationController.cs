using Business.Entites.Parameters;
using Business.Processors;
using Microsoft.AspNetCore.Mvc;
using SQL_Provider.ShoppingDB;

namespace Shopping_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private AuthenticationProcessor _authenticationProcessor = null;
        public static UserParameters userParam = null;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserParameters>> Register(SignupParameters request)
        {
            _authenticationProcessor = new AuthenticationProcessor(_configuration);
            _authenticationProcessor.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            if (!_authenticationProcessor.SaveUser(request, passwordHash, passwordSalt))
                return BadRequest("Username Already Exists !!");

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            _authenticationProcessor = new AuthenticationProcessor(_configuration);

            if (!_authenticationProcessor.VerifyUser(request, out string userID))
                return BadRequest("Wrong Username or Password");

            var token = _authenticationProcessor.CreateToken(request, userID);
            return Ok(token);
        }



    }
}
