using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Duty
    {
        public int Id { get; set; }
        [Display(Name = "Напарниця")]
        public string? Coworker { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Робочі дні")]
        public string? Workday { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "ПІБ")]
        public string Name { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
    }
}
