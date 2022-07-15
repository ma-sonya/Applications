using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication
{
    public partial class Inhabitant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "ПІБ")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name ="Кімната")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Курс")]
        [Range(1,4,ErrorMessage ="Номер вашого курсу може бути від 1 до 4")]
        public int YearStudy { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Факультет")]
        public int FacultyId { get; set; }

        [Display(Name = "Кількість Актів")]
        [Range(0,1000,ErrorMessage ="Кількість актів більше рівна 0")]
        public int? Act { get; set; }

        [Display(Name="Категорія")]
        public virtual Category Category { get; set; } = null!;

        [Display(Name = "Факультет")]
        public virtual Faculty Faculty { get; set; } = null!;

        [Display(Name = "Кімната")]
        public virtual Room Room { get; set; } = null!;        
    }
}
