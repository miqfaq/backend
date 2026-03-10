using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIBackend.Models.Employees;

namespace WebAPIBackend.Models.Departments
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = null;
        public List<Employee> EmployeesList { get; set; }
    }
}