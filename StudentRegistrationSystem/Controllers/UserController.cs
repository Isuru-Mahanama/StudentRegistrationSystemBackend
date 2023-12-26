using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
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
        private readonly IUserRepository userRepository;

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

        public UserController(IConfiguration configuration, IUserRepository userRepository) {
            this.configuration = configuration;
            this.userRepository = userRepository;
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
        //Getting the registered user details
       
        //Login the User

        [HttpPost("login")]
        public ActionResult<TokenDTO> Login(UserLoginDTO request)
        {
            user.email = request.email;
            user.passwordHash = request.passwordHash;
            Console.WriteLine("HI");
            TokenDTO token = new TokenDTO();
            /*   if (superAdmin.email == user.email && superAdmin.passwordHash == user.passwordHash)*/
            if (superAdmin.email == request.email)
            {
                if(superAdmin.passwordHash == request.passwordHash) {
                    
                    user.userType = EnumRoles.Admin;
                    user.userID = 1;
                    string tokenSuperAdmin = createTokensAdmin(user);
                    
                    token.token = tokenSuperAdmin;
                    token.userType = EnumRoles.Admin;
                    return Ok(token);
                }
                if(request.passwordHash== null)
                {
                    token.token = "Your Password is empty";
                    return token;
                }

                token.token = "your Password is wrong";
                return token;
            }
            else
            {
                // Assuming you have a method to check user credentials against the database
                var normalUser = userRepository.GetUserFromDatabase(request.email, request.passwordHash);

                if (normalUser != null)
                {
                    user.userType = EnumRoles.Student;
                    user.userID = normalUser.userID; // Set the appropriate user ID from the database
                    string tokenNormalUser = createTokensUser(user);
                    token.token = tokenNormalUser;
                    token.userType = EnumRoles.Student;
                    return Ok(token);
                }

                token.token = "Invalid email or password for normal user";
                return token ;
            }



            //   string token = createTokensUser(user);
            ///  return Ok(token);
            token.token = "Unauthorized acess";
            return token;

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

        [HttpDelete]
        [Route("admin/getStudentDelete")]  // Corrected the spelling in the Route attribute
        public async Task<User> deleteStudent(int studentID)
        {

            User user = await userRepository.deleteStudent(studentID);  // Corrected the variable name
            return user;  // Return the updated course, not the input parameter
        }



    }
}
