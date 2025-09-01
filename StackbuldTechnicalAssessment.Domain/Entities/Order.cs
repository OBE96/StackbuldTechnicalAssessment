namespace StackbuldTechnicalAssessment.Domain.Entities
{
    public class Order : EntityBase
    {
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

       
    }
}
