using ComplexModel.Data;
using ComplexModel.Migrations;
using ComplexModel.Models;
using ComplexModel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplexModel.Areas.Customer.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly DatabaseContext _context;
        private List<Item> uniqueItems;

        [BindProperty]
        public CustomerOrderVM customerOrderVM { get; set; }
        //public  customerOrderController(DatabaseContext context)
        //{
        //    _context = context;
        //    //placeOrderVM = new CustomerOrderVM()
        //    //{
        //    //    Items=await _context.Items.Include(x=>x.UnitItems).ToListAsync(),
        //    //    //Units=_context.Units.ToList(),
        //    //};
        //}
        public CustomerOrderController(DatabaseContext context)
        {
            _context    = context;
            //customerOrderVM = new CustomerOrderVM()
            //{
            //    UnitItems = _context.UnitItems.ToList()
            //};
        }
        public IActionResult Index()
        {
            //var items = _context.Items.ToList();
            //return View(items);
            var items = (from item in _context.Items

                         join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
                         join unit in _context.Units on ItemUnit.UnitId equals unit.UnitId
                         select new ItemVm
                         {
                             ItemId = item.Id,
                             UnitId = unit.UnitId,
                             ItemName = item.Name,
                             PricePerUnit = ItemUnit.PricePerUnit,
                             UnitType = unit.UnitType,
                             Quantity = ItemUnit.Quatity
                         });

            return View(items);

        }
        [HttpGet]
        public async Task<IActionResult> placeOrder()
        {
            //var Items = await _context.Items.Include(x => x.UnitItems).ThenInclude(x=>x.Unit).ToListAsync();


            //var items = (from item in _context.Items

            //             join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
            //             join unit in _context.Units on ItemUnit.UnitId equals unit.UnitId
            //             select new CustomerOrderVM
            //             {

            //                 Items = _context.Items.ToList(),
            //                 Units = 
            //             });

            var items = (from item in _context.Items

                         join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
                         //join unit in _context.Units on ItemUnit.UnitId equals unit.UnitId
                         select new Item()
                         {
                             Id = item.Id,
                             Name = item.Name,
                             
                         });

            //var units = from unit in _context.Units
            //            join ItemUnit in _context.UnitItems on unit.UnitId equals ItemUnit.UnitId
            //            select new Unit()
            //            {
            //                UnitId = unit.UnitId,
            //                UnitType = unit.UnitType,
            //            };
            //var uniqueUnits = units.Distinct().ToList();
            ////var UnitItem = _context.UnitItems.ToList();
            //var Items = _context.Items.ToList();

            //   context.Entry(student)
            //.Collection(s => s.StudentCourses)
            //.Query()
            //    .Where(sc => sc.CourseName == "Maths")
            //    .FirstOrDefault();
            var uniqueItems = items.Distinct().ToList();


            customerOrderVM = new CustomerOrderVM()
            {
                Items = uniqueItems,
                //UnitItems = _context.UnitItems.ToList()
                //Units = uniqueUnits

            };
            return View(customerOrderVM);
        }


        //[HttpPost]
        //public IActionResult placeOrder(CustomerOrderVM model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public IActionResult getUnits(int id)
        {
            //var items = (from item in _context.Items

            //             join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
            //             join unit in _context.Units on ItemUnit.UnitId equals unit.UnitId
            //             select new ItemVm
            //             {
            //                 ItemId = item.Id,
            //                 UnitId = unit.UnitId,
            //                 ItemName = item.Name,
            //                 PricePerUnit = ItemUnit.PricePerUnit,
            //                 UnitType = unit.UnitType,
            //                 Quantity = ItemUnit.Quatity
            //             });


            //var units = from unit in _context.Units
            //            join unitItem in _context.UnitItems on unit.UnitId equals unitItem.UnitId
            //            join item in _context.Items on unitItem.ItemId equals id
            //            select new Unit()
            //            {
            //                UnitId= unit.UnitId,
            //                UnitType= unit.UnitType,
            //            };
            //var units = from item in _context.Items
            //            join UnitItems in _context.UnitItems on item.Id equals id
            //            join unit in _context.Units on UnitItems.UnitId equals unit.UnitId
            //            select new Unit()
            //            {
            //                UnitId = unit.UnitId,
            //                UnitType = unit.UnitType,
            //            };
            //var units = from item in _context.Items
            //            join UnitItems in _context.UnitItems on item.Id equals id
            //            join unit in _context.Units on UnitItems.UnitId equals unit.UnitId
            //            select new UnitItem()
            //            {
            //                Item = item,
            //                Unit = unit,
            //            };
            var unitItems = _context.UnitItems.Where(ui=>ui.ItemId==id).ToList();
            List<Unit> Units = new List<Unit>();

            //List<int> UnitIds= new List<int>();
                
            //foreach (var ui in unitItems)
            //{
            //    //UnitIds.Append(ui.UnitId);
               
            //}
            //var units = _context.Units.Where(u=>u.UnitId==UnitIds).ToList();
            //var uniqueUnits = units.Distinct().ToList();
            //customerOrderVM.Units= uniqueUnits;
           //{
           //     Units = uniqueUnits
           //};
            return Ok();
            

        }
    }
}
