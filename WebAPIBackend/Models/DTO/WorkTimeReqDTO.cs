namespace WebAPIBackend.Models.DTO
{
    public class WorkTimeReqDTO
    {
        public int EmployeeId {  get; set; }
        public int WorkTime { get; set; } = 0;
        public string? Description { get; set; } = null;

    }
}