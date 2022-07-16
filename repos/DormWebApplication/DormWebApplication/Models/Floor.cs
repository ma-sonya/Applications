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
        public int ChiefId { get; set; }
        [Display(Name = "Відкрита кухня?")]
        public string? OpenKitchen { get; set; }

        public virtual Inhabitant Chief { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
