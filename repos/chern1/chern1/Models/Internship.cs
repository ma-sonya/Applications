using System.ComponentModel.DataAnnotations.Schema;

namespace chern1.Models
{
    [Table("Internships")]
    public class Internship: Employee
    {
        public DateTime WorkTo { get; set; }
    }
}
