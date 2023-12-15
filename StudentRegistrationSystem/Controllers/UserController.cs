using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
                passwordHash = "superAdmin",
                userType = EnumRoles.Admin
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
        public ActionResult<string> Login(UserLoginDTO request)
        {
            user.email = request.email;
            user.passwordHash = request.passwordHash;
            Console.WriteLine("HI");

            /*   if (superAdmin.email == user.email && superAdmin.passwordHash == user.passwordHash)*/
            if (superAdmin.email == request.email)
            {
                if(superAdmin.passwordHash == request.passwordHash) {
                    
                    user.userType = EnumRoles.Admin;
                    user.userID = 1;
                    string tokenSuperAdmin = createTokensAdmin(user);
                    return Ok(tokenSuperAdmin);
                }
                if(request.passwordHash== null)
                {
                    return "your password is empty";
                }
                return "your Password is wrong";
            }
            
           
            
         //   string token = createTokensUser(user);
          ///  return Ok(token);
            return "Unauthorized acess";

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
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: cred
        );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
