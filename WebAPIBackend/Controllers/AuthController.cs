using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.Users;
using WebAPIBackend.Utils;

namespace WebAPIBackend.Controllers
{
    
    //[ApiController]
    public class AuthController : ControllerBase
    {
        //private List<User> _users = new List<User>
        //{
        //    new User{Name = "admin", Password="1234", Role = "admin"},
        //    new User{Name = "user", Password="123", Role = "user"}
        //};

        private AplicationContext _context;

        public AuthController()
        {
            _context = new AplicationContext();
        }

        [HttpPost("/login")]
        public IActionResult Login(string name, string password)
        {
            var identity = GetIdentity(name, password);
            if (identity == null)
            {
                return BadRequest(new { error_text = "Invalid username or password" });
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken
                (
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSummetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedjwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedjwt,
                username = identity.Name,
            };
            return Ok(JsonConvert.SerializeObject(response));
        }

        private ClaimsIdentity GetIdentity(string name, string password)
        {
            //var user = _users.FirstOrDefault(u => u.Name == name & u.Password == password);

            var user = _context.Users.FirstOrDefault(u => u.Name == name);

            if(user == null)
            {
                return null;
            }

            if(!AuthUtils.VerifyPassword(password, user.Password))
            {
                return null;
            }

            //var tool = _context.Tools
            //    .Include(x => x.WorkTimeList)
            //    .FirstOrDefault(t => t.Id == user.Id);

            var Claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

            var claimsIdentity
                = new ClaimsIdentity(Claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }


        [HttpPost("/register")]
        public IActionResult Register(string name, string password)
        {
            _context.Users.Add(new User
            {
                Name = name,
                Password = AuthUtils.HashPassword(password),
                Role = "user"
            });
            var id = _context.SaveChanges();
            return Ok(id);

            
        }
            

    }
}
