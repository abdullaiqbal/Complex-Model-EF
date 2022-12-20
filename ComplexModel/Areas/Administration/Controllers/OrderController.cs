using ComplexModel.Data;
using ComplexModel.Models;
using ComplexModel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
        public IActionResult EditItems(int id)
        {
          
            OrderedItem item = _context.OrderedItems.Where(oi => oi.Id == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        public IActionResult EditItems(OrderedItem model)
        {
            var order = _context.Orders.Where(o=>o.OrderId == model.OrderId ).FirstOrDefault();
            //order.TotalPrice = order.TotalPrice - model.Sub_Total;
            var quantity = model.Quantity;
            var unitItem = _context.UnitItems.Where(ui => ui.ItemId == model.ItemId && ui.UnitId == model.UnitId).FirstOrDefault();
            var pricePerUnit = unitItem.PricePerUnit;
            var subTotal = pricePerUnit * quantity;
            model.Sub_Total = subTotal;
            if (model.customerName != null)
            {
                order.CustomerName = model.customerName;
               //List<OrderedItem> orderedItem = _context.OrderedItems.Where(oi => oi.OrderId == order.OrderId).ToList();
               //for (int i = 0;i<orderedItem.Count(); i++)
               // {
               //     if (orderedItem[i].Id != model.Id)
               //     {
               //         orderedItem[i].customerName = model.customerName;
               //         _context.OrderedItems.Update(orderedItem[i]);
               //         _context.SaveChanges();
               //     }
               // }
               
            }
            //var orderedItemId = model.Id;
            
           
            _context.OrderedItems.Update(model);
            _context.SaveChanges();
            
            List<OrderedItem> oi = _context.OrderedItems.Where(o=>o.OrderId ==model.OrderId).ToList();
            decimal? totalPrice = 0;
            if (oi != null)
            {
                foreach(var i in oi)
                {
                    totalPrice = totalPrice + i.Sub_Total;
                }
            }
            order.TotalPrice = totalPrice;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }



        [HttpGet]
        public IActionResult DeleteOrderedItems(int id)
        {
            //var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            var OItem = _context.OrderedItems.Where(oi => oi.OrderId ==id).ToList();


            return View(OItem);
        }


        //[HttpPost]
        public IActionResult DeleteSingleOrderItem(int id)
        {
            
            var item = _context.OrderedItems.Where(oi=>oi.Id == id).FirstOrDefault();   
            var order = _context.Orders.Where(o=>o.OrderId ==item.OrderId).FirstOrDefault();
            var totalPrice = order.TotalPrice;
            totalPrice = totalPrice - item.Sub_Total;
            order.TotalPrice = totalPrice;
            _context.Orders.Update(order);
            _context.OrderedItems.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //        {
        //            (int OrderId, int ItemId, int UnitId) value = (oi.OrderId, oi.ItemId, oi.UnitId);
        //            return new int () { value. };
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
