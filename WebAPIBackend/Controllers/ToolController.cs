
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.Departments;
using WebAPIBackend.Models.DTO;
using WebAPIBackend.Models.Employees;
using WebAPIBackend.Models.Tools;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolController : ControllerBase
    {
        private AplicationContext _context;
        public ToolController()
        {
            _context = new AplicationContext();
        }

        [HttpGet("tools")]
        public IEnumerable<Tool> GetTools()
        {
            return _context.Tools.Include(t=>t.WorkTimeList);
        }
        [HttpGet("tool")]
        public IActionResult CreateTool([FromBody] ToolsDto toolsDto)
        {
            var tools_list = _context.Departments.Include(d => d.ToolsList).FirstOrDefault(d => d.Id == toolsDto.DepartmentId);
            _context.SaveChanges();
            if (tools_list != null)
            {
                var _tool = _context.Tools.Add(toolsDto.Tools);
                _context.SaveChanges();
                if (tools_list.ToolsList == null)
                {
                    tools_list.ToolsList = new List<Tool>();
                }
                tools_list.ToolsList.Add(_tool.Entity);
                _context.SaveChanges();
                return Ok(_tool.Entity.Id);
            }
            return BadRequest("Department not found")

        }

        [HttpPut("tool")]
        public IActionResult UpdateTool([FromBody] Tool tool)
        {
            var _tool = _context.Tools.FirstOrDefault(e => e.Id == tool.Id);
            if (_tool != null)
            {
                _tool.Name = tool.Name;
                _tool.Description = tool.Description;
                _tool.status = tool.status;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Tool not found");

        }
        
    }
}