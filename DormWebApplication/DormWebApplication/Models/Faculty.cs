using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Faculty
    {
        public Faculty()
        {
            Inhabitants = new HashSet<Inhabitant>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва факультету")]
        public string? Name { get; set; }

        public virtual ICollection<Inhabitant> Inhabitants { get; set; }
    }
}
