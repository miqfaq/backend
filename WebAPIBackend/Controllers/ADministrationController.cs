using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.DTO;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.DTO;
using WebAPIBackend.Models.Employees;
using WebAPIBackend.Models.Users;

namespace WebAPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private AplicationContext _context;
        public AdministrationController()
        {
            _context = new AplicationContext();
        }

        [HttpGet("roles")]
        [Authorize(Roles = "admin")]
        public IEnumerable<string> GetRoles()
        {
            return new List<string>{
                "admin",
                "manager",
                "user"
            };
        }

        [HttpGet("getusers")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _context.Users.ToList().OrderBy(u => u.Id);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("setuserrole")]
        [Authorize(Roles = "admin")]
        public IActionResult SetRoleToUser([FromBody] SetRoleRequestDto setRoleRequestDto)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == setRoleRequestDto.UserId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                user.Role = setRoleRequestDto.RoleName;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}