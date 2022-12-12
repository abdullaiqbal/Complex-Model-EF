using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ComplexModel.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Provide Unit Type Like Litter or Kg")]
        [Display(Name = "Unit Type")]

        [StringLength(50, MinimumLength = 2)]
        public string UnitType { get; set; }
        //// public virtual ICollection<Item> Items { get; set; }
        //public ICollection<Item> items { get; set; }
        public virtual ICollection<UnitItem>? UnitItems { get; set; }
        public virtual ICollection<OrderedItem>? OrderedItems { get; set; }

    }
}
