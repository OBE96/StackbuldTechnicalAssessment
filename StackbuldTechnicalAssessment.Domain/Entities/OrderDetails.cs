using System.ComponentModel.DataAnnotations;

namespace StackbuldTechnicalAssessment.Domain.Entities
{
    public class OrderDetails : EntityBase
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
