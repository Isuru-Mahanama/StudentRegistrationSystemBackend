using Microsoft.AspNetCore.Authorization;
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

        //SuperAdmin

        private static User superAdmin;

        static UserController()
        {
            // Initialize super admin details
            superAdmin = new User
            {
                userID = 1,
                email = "superadmin@example.com",
                passwordHash = BCrypt.Net.BCrypt.HashPassword("superadminpassword"),
                userType = "Admin"
            };
        }

        public UserController(IConfiguration configuration) {
            this.configuration = configuration;
        }
        //Registering the User
        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.passwordHash);

            user.email = request.email;
            user.passwordHash = passwordHash;
            
            return Ok(user);

        }

        //Login the User

        [HttpPost("login")]
        public ActionResult<User> Login(UserDTO request)
        {
            Console.WriteLine("HI");

            if (superAdmin.email == request.email && superAdmin.passwordHash == request.passwordHash)
            {
                string tokenSuperAdmin = createTokensAdmin(user);
                return Ok(tokenSuperAdmin);
            }

            if (user.email != request.email)
            {
                return BadRequest("User not found");
            }

           if(!BCrypt.Net.BCrypt.Verify(request.passwordHash ,user.passwordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = createTokensUser(user);
            return Ok(token);

        }

        [HttpPost("admin/login")]
        public ActionResult<User> AdminLogin(UserDTO request)
        {
            if (user.email != request.email)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.passwordHash, user.passwordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = createTokensAdmin(user);
            return Ok(token);

        }
        private string createTokensUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role,"User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
        );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        private string createTokensAdmin(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

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
