using Business.Entites.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SQL_Provider.ShoppingDB;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Business.Processors
{
    public class AuthenticationProcessor
    {
        private readonly IConfiguration _configuration;
        private ShoppingContext _shoppingContext;
        private static User user = null;

        public AuthenticationProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SaveUser(SignupParameters userParams, byte[] passwordHash, byte[] passwordSalt )
        {
            user = new User();
            _shoppingContext = new ShoppingContext(_configuration);
            var existUsername = _shoppingContext.Users.Where(ob => ob.UserName.Contains(userParams.UserName));
            if (existUsername.FirstOrDefault() != null)
                return false;

            user.ID = Guid.NewGuid();
            user.UserName = userParams.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Email = userParams.Email;
            user.Gender = userParams.Gender;
            user.Email = userParams.Email;
            user.Birthdate = userParams.Birthdate;

            _shoppingContext.Users.Add(user);
            _shoppingContext.SaveChanges();
            return true;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }     
        
        public bool VerifyUser(UserDto userDto)
        {
            _shoppingContext = new ShoppingContext(_configuration);
            bool isUserVerified = false;
            var user = _shoppingContext.Users.Where( ob => ob.UserName.Contains(userDto.UserName));

            if (user.FirstOrDefault() != null)
            {
                byte[] passwordHash = user.FirstOrDefault().PasswordHash;
                byte[] passwordSalt = user.FirstOrDefault().PasswordSalt;

                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
                    bool isPasswordVerified =  computedHash.SequenceEqual(passwordHash);
                    isUserVerified = isPasswordVerified;
                }
            }
            return isUserVerified;
        }

        public string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
