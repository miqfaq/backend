
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIBackend.Models.Tools
{
    public class WorkTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Description { get; set; }
        //public Tool Tool { get; set; }
        public WorkTime() 
        {
            DateRegistration = DateTime.Now;
        }
    }
}