using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPIBackend.Models.Users;
using Newtonsoft.Json;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private List<User> _users = new List<User>
        {
            new User{Name = "admin", Password="1234", Role = "admin"},
            new User{Name = "user", Password="123", Role = "user"}
        };
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
            var user = _users.FirstOrDefault(u => u.Name == name & u.Password == password);
            if (user != null) 
            {
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

            };
            return null;
        }
            

    }
}
