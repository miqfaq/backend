using WebAPIBackend.Models.Tools;

namespace WebAPIBackend.Models.DTO
{
    public class ToolsDto
    {
        public int DepartmentId { get; set; }
        public Tool Tools { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
    }
}