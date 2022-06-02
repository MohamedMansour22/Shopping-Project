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
        public static User user = new User();
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            _authenticationProcessor = new AuthenticationProcessor(_configuration);
            _authenticationProcessor.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.UserName = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            _authenticationProcessor = new AuthenticationProcessor(_configuration);

            if (request.Username != user.UserName)
                return BadRequest("Wrong username");

            if(!_authenticationProcessor.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong Password");

            var token = _authenticationProcessor.CreateToken(user);
            return Ok(token);
        }



    }
}
