
using Microsoft.AspNetCore.Mvc;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.Departments;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private AplicationContext _context;

        public DepartmentsController()
        {
            _context = new AplicationContext();
        }
        [HttpGet]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        [HttpPost("department")]
        public IActionResult AddDepartment(string name, string? desctiption)
        {
            var department = _context.Departments.Add(new Department { Name = name, Description = desctiption });
            _context.SaveChanges();
            return Ok(department.Entity?.Id ?? 0);
        }

        [HttpPut("department")]
        public IActionResult UpdateDepartment (int id, string name, string? description)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);
            if(department != null)
            {
                department.Name = name;
                department.Description = description;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Department not found");
        }
        [HttpDelete("department")]
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