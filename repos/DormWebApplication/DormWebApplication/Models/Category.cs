using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Category
    {
        public Category()
        {
            Duties = new HashSet<Duty>();
            Inhabitants = new HashSet<Inhabitant>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name = "Категорія мешканця")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Вартість проживання (грн\\міс)")]
        public string Price { get; set; } = null!;

        public virtual ICollection<Duty> Duties { get; set; }
        public virtual ICollection<Inhabitant> Inhabitants { get; set; }
    }
}
