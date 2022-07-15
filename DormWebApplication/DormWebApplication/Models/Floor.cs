using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Floor
    {
        public Floor()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }

        [Display(Name = "Поверх")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(0,100,ErrorMessage ="Номер поверху має бути більше 0")]
        public int FloorNumber { get; set; }

        [Display(Name = "Староста")]
        public string? ChiefId { get; set; }

        [Display(Name = "Чи кухню відкрито?")]
        public bool IsKitchenOpen { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
