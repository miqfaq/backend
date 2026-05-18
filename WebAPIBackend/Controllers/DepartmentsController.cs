
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.Departments;
using WebAPIBackend.Models.DTO;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private AplicationContext _context;

        public DepartmentsController()
        {
            _context = new AplicationContext();
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        [HttpPost("department")]
        [Authorize(Roles ="admin")]
        public IActionResult AddDepartment([FromBody]AddDepReqDto addDepReqDTO)
        {
            var department = _context.Departments.Add(new Department { Name = addDepReqDTO.name, Description = addDepReqDTO.description });
            _context.SaveChanges();
            return Ok(department.Entity?.Id ?? 0);
        }

        [HttpPut("department")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateDepartment([FromBody] UpdDepReqDto updDepReqDto)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == updDepReqDto.Id);
            if(department != null)
            {
                department.Name = updDepReqDto.Name;
                department.Description = updDepReqDto.Description;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Department not found");
        }
        [HttpDelete("department")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Department not found");
        }
    }
}