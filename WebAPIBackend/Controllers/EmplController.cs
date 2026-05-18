using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.Departments;
using WebAPIBackend.Models.DTO;
using WebAPIBackend.Models.Employees;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmplController : ControllerBase
    {
        private AplicationContext _context;

        public EmplController()
        {
            _context = new AplicationContext();
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IEnumerable<Employee> GetEmpls()
        {
            return _context.Employees.ToList();
        }

        [HttpPost("empl")]
        //[Authorize(Roles = "admin")]
        public IActionResult AddEmpl([FromBody] EmplReqDto emplReqDto)
        {
            var empls = _context.Employees.Add(new Employee { FirstName = emplReqDto.FirstName, LastName = emplReqDto.LastName, Birthdate = emplReqDto.Birthdate, DepartmentId = emplReqDto.DepartmentId, MddleName = emplReqDto.MddleName, Email = emplReqDto.Email, PhoneNumber = emplReqDto.PhoneNumber, Post = emplReqDto.Post });
            _context.SaveChanges();
            return Ok(empls.Entity?.Id ?? 0);
        }

        [HttpPut("empl")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEmpl([FromBody] EmplReqDto emplReqDto)
        {
            var empl = _context.Employees.FirstOrDefault(d => d.Id == emplReqDto.Id);
            if (empl != null)
            {
                empl.FirstName = emplReqDto.FirstName;
                empl.LastName = emplReqDto.LastName;
                empl.Birthdate = emplReqDto.Birthdate;
                empl.DepartmentId = emplReqDto.DepartmentId;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Department not found");
        }
        [HttpDelete("empl")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEmpl(int id)
        {
            var empl = _context.Employees.FirstOrDefault(d => d.Id == id);
            if (empl != null)
            {
                _context.Employees.Remove(empl);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Department not found");
        }
    }
}