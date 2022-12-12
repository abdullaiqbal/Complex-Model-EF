using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ComplexModel.Models.ViewModels
{
    public class AddItemDetailsVm
    {
        //public string ItemName { get; set; }
        //public string? UnitType { get; set; }
        //This is ItemId
       
        
        public Decimal PricePerUnit { get; set; }
        public int Quatity { get; set; }
        public int ItemId { get; set; }
        public ICollection<Item>? Items { get; set; }
        public int UnitId { get; set; }
        public ICollection<Unit>? Units { get; set; }

        //For Select List Item

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


        public IEnumerable<SelectListItem> CSelectListUnit(IEnumerable<Unit> Items)
        {
            List<SelectListItem> listUnits = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listUnits.Add(Sli);
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



    }
}
