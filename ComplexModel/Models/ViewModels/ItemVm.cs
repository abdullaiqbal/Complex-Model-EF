using System.ComponentModel.DataAnnotations;

namespace ComplexModel.Models.ViewModels
{
    public class ItemVm
    {
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public string ItemName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string UnitType { get; set; }
        public Decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
    }
}
