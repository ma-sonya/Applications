using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Furniture
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
