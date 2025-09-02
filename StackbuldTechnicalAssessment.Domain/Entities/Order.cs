using System.ComponentModel.DataAnnotations;

namespace StackbuldTechnicalAssessment.Domain.Entities
{
    public class Order : EntityBase
    {
        [Required]
        public string PickupName { get; set; }

        [Required]
        public string PickupPhoneNumber { get; set; }

        [Required]
        public string PickupEmail { get; set; }
        public decimal OrderTotal { get; set; }

        public int TotalItems { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();


    }
}
