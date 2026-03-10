using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIBackend.Models.Tools
{
    public class Tool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = null;
        public bool status { get; set; } = false;//Занято ли оборудование
        public List<WorkTime> WorkTimeList { get; set; }
    }
}