﻿namespace ComplexModel.Models
{
    public class OrderedItem
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }


        public Unit Unit { get; set; }
        public int UnitId { get; set; }

        public String? customerName { get; set; }
        public string? CustomerGuidKey { get; set; }
        public int? Quantity { get; set; }
        public decimal? Sub_Total { get; set; }

    }
}
