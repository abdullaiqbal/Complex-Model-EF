using ComplexModel.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComplexModel.Models.ViewModels
{
    public class CustomerOrderVM
    {

        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<Unit>? Units { get; set; }
        public ICollection<UnitItem>? UnitItems { get; set; }
        public string? CustomerName { get; set; }
        public int? Quantity { get; set; }
        public string? CustomerGuidKey { get; set; }
        public Decimal TotalPrice { get; set; }
        //public Decimal PricePerUnit { get; set; }


        //Collections
        //public List<int>? ItemIds { get; set; }
        ////public ICollection<Item> Items { get; set; }
        //public List<int>? UnitIds { get; set; }
        ////public ICollection<Unit> Units { get; set; }

        //public List<int>? Quatities { get; set; }






        //List Items
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Item> Items)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listItems.Add(Sli);
            foreach (Item item in Items)
            {
                Sli = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                };
                listItems.Add(Sli);

            }
            return listItems;
        }

        //public void SetUnits(int? id)
        //{
        //    //var units = from unit in UnitItems
        //    //            where unit.ItemId == id
        //    //            select new Unit
        //    //            {
        //    //                UnitId = unit.UnitId,
        //    //                UnitType = unit.Unit.UnitType
        //    //            };
        //    if (id != null)
        //    {
        //        var units = from unit in UnitItems
        //                    where unit.ItemId == id
        //                    select new Unit
        //                    {
        //                        UnitId = unit.UnitId,
        //                        UnitType = unit.Unit.UnitType
        //                    };
        //        var uniqueUnits = units.Distinct().ToList();
        //        Units = uniqueUnits;
        //    }
           
            
        //} 

        public IEnumerable<SelectListItem>? CSelectListUnit(IEnumerable<Unit>? Items)
        {
            List<SelectListItem> listUnits = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listUnits.Add(Sli);
            if (Items != null)
            {


                foreach (Unit item in Items)
                {
                    Sli = new SelectListItem
                    {
                        Text = item.UnitType,
                        Value = item.UnitId.ToString()
                    };
                    listUnits.Add(Sli);

                }
            return listUnits;
            }
            return null;
        }
    }
}
