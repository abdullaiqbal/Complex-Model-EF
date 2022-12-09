namespace ComplexModel.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public int TotalQuatity { get; set; }
        public virtual ICollection<UnitItem>? UnitItems { get; set; } /*= new HashSet<UnitItem>();*/
        //public virtual Unit Unit { get; set; }
        //public int UnitId { get; set; }
        public virtual ICollection<OrderedItem>? OrderedItems { get; set; } /*= new List<OrderedItem>();*/
    }
}
