using System.ComponentModel.DataAnnotations;

namespace BookStore_WebApplication.Models
{
    public class BookOrderDto
    {
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name ="Книга")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Range(1, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Кількість")]

        public int Number { get; set; }
        public Order Order { get; set; }
    }
}
