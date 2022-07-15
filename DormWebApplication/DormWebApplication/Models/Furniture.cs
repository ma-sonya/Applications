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

        [Display(Name="Кількість")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(0,10, ErrorMessage ="Кількість меблів може бути від 0 до 10")]
        public int Number { get; set; }

        [Display(Name = "Кімната")]
        public int? RoomId { get; set; }

        [Display(Name="Кімната")]
        public virtual Room Room { get; set; } = null!;
    }
}
