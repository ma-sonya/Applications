namespace DrugstoreAPIWebApp.Models
{
    public class Drug
    {
        public Drug()
        {
            ProducerDrugs = new List<ProducerDrug>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection <ProducerDrug> ProducerDrugs { get; set; } 
    }
}
