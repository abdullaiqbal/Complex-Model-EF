namespace ComplexModel.Models.ViewModels
{
    public class OrderedItemsVM
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public string ItemName { get; set; }
        public string UnitType { get; set; }

        public String? customerName { get; set; }
        public string? CustomerGuidKey { get; set; }
        public int? Quantity { get; set; }
        public decimal? Sub_Total { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
