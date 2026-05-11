
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
            return _context.Tools.Include(t => t.WorkTimeList);
        }
        [HttpPost("tool")]
        public IActionResult CreateTool([FromBody] ToolsDto toolsDto)
        {
            // Сначала находим департамент
            var department = _context.Departments
                .Include(d => d.ToolsList)
                .FirstOrDefault(d => d.Id == toolsDto.DepartmentId);

            if (department == null)
            {
                return BadRequest("Department not found");
            }

            // Создаем новый инструмент
            var newTool = new Tool
            {
                Name = toolsDto.Name,
                Description = toolsDto.Description,
                status = toolsDto.status,
                WorkTimeList = new List<WorkTime>() // Инициализируем список
            };

            // Добавляем инструмент в контекст
            _context.Tools.Add(newTool);

            // Добавляем инструмент в список департамента
            if (department.ToolsList == null)
            {
                department.ToolsList = new List<Tool>();
            }
            department.ToolsList.Add(newTool);

            _context.SaveChanges();

            return Ok(newTool.Id);
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

        [HttpDelete("tool")]
        public IActionResult DeleteTool([FromBody] Tool tool)
        {
            var _tool = _context.Tools.FirstOrDefault(e => e.Id == tool.Id);
            if ( _tool != null)
            {
                _context.Tools.Remove(_tool);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Tool not found");
        }

        [HttpPost("worktime")]
        public IActionResult AddWorkTime([FromBody] WorkTime workTime)
        {
            var _tool = _context.Tools.FirstOrDefault(e =>e.Id == workTime.Id);
            if (_tool != null)
            {
                if ( _tool.WorkTimeList == null)
                {
                    _tool.WorkTimeList = new List<WorkTime>();
                }
                _tool.WorkTimeList.Add(new WorkTime
                {
                    Description = workTime.Description,
                    DateRegistration = workTime.DateRegistration,
                });
                _context.SaveChanges();
                var WtId = _context.WorkTime.LastOrDefault()?.Id ?? 0;
                return Ok(WtId);
            }
            return BadRequest("Tool not found");
        }

        [HttpDelete("worktime")]
        public IActionResult DeleteWorkTime(int id)
        {
            var _workTime = _context.WorkTime.FirstOrDefault(e => e.Id == id);
            if (_workTime != null)
            {
                _context.WorkTime.Remove(_workTime);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Work Time not found");
        }



    }
}