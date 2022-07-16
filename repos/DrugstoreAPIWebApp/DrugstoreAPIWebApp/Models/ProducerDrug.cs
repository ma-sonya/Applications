namespace DrugstoreAPIWebApp.Models
{
    public class ProducerDrug
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual Drug Drug { get; set; }
    }
}
