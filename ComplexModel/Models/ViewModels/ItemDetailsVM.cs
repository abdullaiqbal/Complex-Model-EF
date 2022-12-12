namespace ComplexModel.Models.ViewModels
{
    public class ItemDetailsVM
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        //public string? UnitType { get; set; }
        //public int? Quatity { get; set; }
        public IEnumerable<UnitItem>? UnitItmes { get; set; }
    }
}
