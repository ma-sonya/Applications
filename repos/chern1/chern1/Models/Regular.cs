using System.ComponentModel.DataAnnotations.Schema;

namespace chern1.Models
{
    [Table("Regulars")]
    public class Regular: Employee
    {
        public Position Post { get; set; }
    }
}
