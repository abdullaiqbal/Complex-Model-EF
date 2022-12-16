using ComplexModel.Data;
using ComplexModel.Models;
using ComplexModel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplexModel.Areas.Administration.Controllers
{
    public class OrderController : Controller
    {
        private readonly DatabaseContext _context;
        public OrderController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Orders.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult OrderedItem()
        {
            var items = _context.OrderedItems.ToList();
            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            IEnumerable<OrderedItemsVM> orderDetails = from Order in _context.Orders
                                                       where Order.OrderId == id
                                                       join orderedItem in _context.OrderedItems on Order.OrderId equals orderedItem.OrderId
                                                       join Item in _context.Items on orderedItem.ItemId equals Item.Id
                                                       join Unit in _context.Units on orderedItem.UnitId equals Unit.UnitId
                                                       select new OrderedItemsVM()
                                                       {
                                                           OrderId = Order.OrderId,
                                                           ItemId = orderedItem.ItemId,
                                                           UnitId = orderedItem.UnitId,
                                                           customerName = orderedItem.customerName,
                                                           ItemName = Item.Name,
                                                           UnitType = Unit.UnitType,
                                                           CustomerGuidKey = orderedItem.CustomerGuidKey,
                                                           Quantity = orderedItem.Quantity,
                                                           Sub_Total = orderedItem.Sub_Total,
                                                           TotalPrice = Order.TotalPrice

                                                       };

            //IEnumerable<Order> orderDetails = _context.Orders.Include(o => o.OrderItem)
            //                                                 .ThenInclude(oi => oi.Item)
            //                                                 .Include(u => u.OrderItem)
            //                                                 .ThenInclude(u => u.Unit)
            //                                                 .Where(o => o.OrderId == id).ToList();


            return View(orderDetails);
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }

        //[HttpGet]
        //public IActionResult DeleteOrderedItem(List<int> id)
        //{
        //    //var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
        //    var OItem = _context.OrderedItems.Where(oi => oi.OrderId)
            
        //    return View(OItem);
        //}

        //{
        //    (int OrderId, int ItemId, int UnitId) value = (oi.OrderId, oi.ItemId, oi.UnitId);
        //    return new int() { value. };
        //})

        //[HttpPost]
        //public IActionResult DeleteOrderedItem(int id)
        //{
        //    //var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
        //    var item = _context.OrderedItems.
        //    return View(item);
        //}

        /// <summary>
        /// Post Secion of course
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Create(Order model)
        {
            model.OrderDate = DateTime.UtcNow;
            _context.Orders.Add(model);
            _context.SaveChanges();
            //return redire
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Order model)
        {
            _context.Orders.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Order model)
        {
            _context.Orders.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
