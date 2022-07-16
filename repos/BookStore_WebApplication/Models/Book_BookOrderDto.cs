using System.ComponentModel.DataAnnotations;

namespace BookStore_WebApplication.Models
{
    public class Book_BookOrderDto
    {
        public int BookId { get; set; }
        [Display(Name = "Книга")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public decimal Cost { get; set; }
        [Display(Name = "Тип / Жанр")]
        public string Type { get; set; } = null!;
        [Display(Name = "Магазин")]
        public int IdStore { get; set; }
        [Display(Name = "Кількість")]
        public int Number { get; set; }

        [Display(Name = "Має автора")]
        public bool IsOrderedBook { get; set; }

        public Book_BookOrderDto(int _id, string _name, decimal _cost, string _type, int _storeId, int _number, bool _isOrderedBook)
        {
            BookId = _id;
            Name = _name;
            Cost = _cost;
            Type = _type;
            IdStore = _storeId;
            Number = _number;
            IsOrderedBook = _isOrderedBook;
        }
    }
}
