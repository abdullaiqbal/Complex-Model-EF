using ComplexModel.Data;
using ComplexModel.Models;
using ComplexModel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComplexModel.Areas.Administration.Controllers
{
    public class ItemController : Controller
    {
        private readonly DatabaseContext _context;
        [BindProperty]
        public AddItemDetailsVm createVM { get; set; }
        //[BindProperty]
        //public UnitItem unitItem { get; set; }
        public ItemController(DatabaseContext context)
        {
            _context = context;
            //unitItem = new UnitItem()
            //{
            //    Unit = _context.Units.ToList(),
            //    Item= _context.Items.ToList(),
            //    //Makes = _db.Makes.ToList(),
            //    //Model = new Models.Model()
            //};
            createVM = new AddItemDetailsVm()
            {
                Units = _context.Units.ToList(),
                Items = _context.Items.ToList(),
                //Makes = _db.Makes.ToList(),
                //Model = new Models.Model()
            };
        }
        [HttpGet]
        public IActionResult Index()
        {
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
        public IActionResult RegisteredItems()
        {
            var items = _context.Items.ToList();

            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddItemDetails()
        {

            return View(createVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _context.Items.Include(ui => ui.UnitItems).ThenInclude(u => u.Unit).Where(x => x.Id == id).FirstOrDefault();
            var ItemsVm = new ItemDetailsVM()
            {
                ItemId = item.Id,
                Name = item.Name,
                //UnitItmes = items[0].UnitItems.ToList(),
                //Quatity = item.UnitItems.Where(u=>u.ItemId==id),
                UnitItmes = item.UnitItems.Where(u => u.ItemId == id),
            };
            //return View(item);
            return View(ItemsVm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
        }

        /// <summary>
        /// Post Secion of course
        /// </summary>
        /// <returns></returns>

        //[HttpPost]
        //public IActionResult Create(Item model)
        //{
        //    _context.Items.Add(model);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        public IActionResult Create(Item model)
        {

            _context.Items.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult AddItemDetails(AddItemDetailsVm model)
        {
            if (ModelState.IsValid)
            {
                UnitItem unitItem = new UnitItem()
                {
                    ItemId = model.ItemId,
                    UnitId= model.UnitId,
                    PricePerUnit = model.PricePerUnit,
                    Quatity = model.Quatity
                };
                _context.UnitItems.Add(unitItem);
                _context.SaveChanges();
            }
            //_context.Items.Add(model);
            //_context.SaveChanges();
            return RedirectToAction("Index");

        }


        //[HttpPost]
        public IActionResult DeleteItemDetails(int id)
        {
            var UnitItemDetails = _context.UnitItems.Where(ui=>ui.UnitId== id).FirstOrDefault();
            _context.UnitItems.Remove(UnitItemDetails);
            //_context.Items.Remove(model);
            _context.SaveChanges();
            //_context.Update(UnitItem);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Edit(Item model)
        {
            _context.Items.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Item model)
        {
            _context.Items.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
