using System.ComponentModel.DataAnnotations;

namespace DrugstoreAPIWebApp.Models
{
    public class Category
    {
        public Category()
        {
            Drugs = new List<Drug>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не може бути порожнім! Виправте це.")]
        [Display(Name ="Категорія")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
