using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIBackend.Models.Users;
namespace WebAPIBackend.Models.Employees
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Post { get; set; } = null;
        public User User { get; set; }
    }
}