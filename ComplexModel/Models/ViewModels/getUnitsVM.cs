using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComplexModel.Models.ViewModels
{
    public class getUnitsVM
    {
        public int? UnitId { get; set; }
        public ICollection<Unit>? Units { get; set; }
        public int? Quantity { get; set; }




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
