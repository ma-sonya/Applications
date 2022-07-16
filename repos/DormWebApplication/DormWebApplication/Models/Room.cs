using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Room
    {
        public Room()
        {
            Furnitures = new HashSet<Furniture>();
            Inhabitants = new HashSet<Inhabitant>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "К-сть місць")]
        public int CountPlace { get; set; }
        public int FloorId { get; set; }
        [Display(Name = "Мешканці")]
        public string? Resident { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public string Number { get; set; } = null!;

        public virtual Floor Floor { get; set; } = null!;
        public virtual ICollection<Furniture> Furnitures { get; set; }
        public virtual ICollection<Inhabitant> Inhabitants { get; set; }
    }
}
