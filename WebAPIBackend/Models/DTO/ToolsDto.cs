using WebAPIBackend.Models.Tools;

namespace WebAPIBackend.Models.DTO
{
    public class ToolsDto
    {
        public int DepartmentId { get; set; }
        public Tool Tools { get; set; }
    }
}