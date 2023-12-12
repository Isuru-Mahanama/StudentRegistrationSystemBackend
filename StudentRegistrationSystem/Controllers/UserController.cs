using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        public static User user = new User();
        private readonly IConfiguration configuration;

        public UserController(IConfiguration configuration) {
            this.configuration = configuration;
        }
        //Registering the User
        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.password);

            user.email = request.email;
            user.passwordHash = passwordHash;
            
            return Ok(user);

        }

        //Login the User

        [HttpPost("login")]
        public ActionResult<User> Login(UserDTO request)
        {
           if(user.email != request.email)
            {
                return BadRequest("User not found");
            }

           if(!BCrypt.Net.BCrypt.Verify(request.password ,user.passwordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = createTokens(user);
            return Ok(token);

        }
        private string createTokens(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
        );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
