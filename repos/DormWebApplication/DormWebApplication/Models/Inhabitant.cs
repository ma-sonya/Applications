using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Inhabitant
    {
        public Inhabitant()
        {
            Floors = new HashSet<Floor>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "ПІБ")]
        public string Name { get; set; } = null!;
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Курс")]
        public int YearStudy { get; set; }
        public int CategoryId { get; set; }
        public int FacultyId { get; set; }
        [Display(Name = "Кількість Актів")]
        public int? Act { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Faculty Faculty { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Floor> Floors { get; set; }
    }
}
