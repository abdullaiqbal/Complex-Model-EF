using System.ComponentModel.DataAnnotations;

namespace ComplexModel.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }

        //[Required(ErrorMessage = "Provide Order Name")]
        //public string OrderName { get; set; }
        public decimal? TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerGuidKey { get; set; }
        public virtual ICollection<OrderedItem>? OrderItem { get; set; }
    }
}
