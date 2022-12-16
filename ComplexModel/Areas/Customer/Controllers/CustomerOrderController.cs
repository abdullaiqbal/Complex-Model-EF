using ComplexModel.Data;
using ComplexModel.Models;
using ComplexModel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public IActionResult Index(string sortOrder, string searchString)
        {
            //var items = _context.Items.ToList();
            //return View(items);
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewData["UnitTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "UnitType_desc" : "";
            ViewData["QuantitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "Quantity_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            //ViewData["SortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

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
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.ItemName.Contains(searchString)
                                       || i.UnitType.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.ItemName);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.PricePerUnit);
                    break;
                case "UnitType_desc":
                    items = items.OrderByDescending(i => i.UnitType);
                    break;
                case "Quantity_desc":
                    items = items.OrderByDescending(i => i.Quantity);
                    break;
                default:
                    items = items.OrderBy(i => i.ItemName);
                    break;
            }
            return View(items);

        }
        [HttpGet]
        public async Task<IActionResult> placeOrder(string? customerName, string? customerGuid, int oId, Decimal TotalPrice = 0)
        {
            var items = (from item in _context.Items

                         join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
                         select new Item()
                         {
                             Id = item.Id,
                             Name = item.Name,
                             
                         });

            var uniqueItems = items.Distinct().ToList();

            if (customerName != null)
            {
            customerOrderVM = new CustomerOrderVM()
            {
                Items = uniqueItems,
                CustomerName = customerName,
                CustomerGuidKey = customerGuid,
                OrderId= oId,
                TotalPrice = TotalPrice
                //UnitItems = _context.UnitItems.ToList()
                //Units = uniqueUnits

            };

            }
            return View(customerOrderVM);
        }


        [HttpPost]
        public IActionResult placeOrder(CustomerOrderVM model)
        {
            if (ModelState.IsValid)
            {
                var OrderId = model.OrderId;
                //var ItemId = model.ItemId;
                //var UnitId = model.UnitId;
                var totalprice = model.TotalPrice;
                var pricePerUnit = from UnitItem in _context.UnitItems
                            where UnitItem.ItemId == model.ItemId && UnitItem.UnitId == model.UnitId
                            select new
                            {
                                PricePerUnit = UnitItem.PricePerUnit,
                            };
                decimal priceOfItem = 0;
                foreach(var p in pricePerUnit)
                {
                    priceOfItem = p.PricePerUnit;
                };
                var sub_total = priceOfItem * Convert.ToDecimal(model.Quantity);
                OrderedItem orderedItem = new OrderedItem()
                {
                    ItemId = model.ItemId,
                    OrderId= model.OrderId,
                    UnitId = model.UnitId,
                    CustomerGuidKey= model.CustomerGuidKey,
                    customerName=model.CustomerName,
                    Quantity = model.Quantity,
                    Sub_Total = sub_total
                };
                _context.OrderedItems.Add(orderedItem);
                _context.SaveChanges();

                //var subTotals = _context.OrderedItems.Where(oi=>oi.ItemId== ItemId && oi.OrderId== OrderId && oi.UnitId== UnitId)
                //                 .Select(oi=> oi.Sub_Total).ToList();
                //var totalPrice = 0;
                //foreach(var subTotal in subTotals)
                //{

                //}
                Order order = _context.Orders.Where(o =>o.OrderId== OrderId).FirstOrDefault();
               var previousPrice = order.TotalPrice;
               previousPrice += (sub_total+ totalprice);
                order.TotalPrice = previousPrice;

                _context.Orders.Update(order);
                _context.SaveChanges();



            }
            return RedirectToAction("Index");
        }


        //[HttpPost]
        public IActionResult AddItem(CustomerOrderVM model)
        {
            if (ModelState.IsValid)
            {
                var cName = model.CustomerName.ToString();
                var cGuid = model.CustomerGuidKey.ToString();
                var orderId = model.OrderId;

                var pricePerUnit = from UnitItem in _context.UnitItems
                                   where UnitItem.ItemId == model.ItemId && UnitItem.UnitId == model.UnitId
                                   select new
                                   {
                                       PricePerUnit = UnitItem.PricePerUnit,
                                   };
                decimal priceOfItem = 0;
                foreach (var p in pricePerUnit)
                {
                    priceOfItem = p.PricePerUnit;
                };
                var sub_total = priceOfItem * Convert.ToDecimal(model.Quantity);
                OrderedItem orderedItem = new OrderedItem()
                {
                    ItemId = model.ItemId,
                    OrderId = model.OrderId,
                    UnitId = model.UnitId,
                    CustomerGuidKey = model.CustomerGuidKey,
                    customerName = model.CustomerName,
                    Quantity = model.Quantity,
                    Sub_Total = sub_total
                };
                _context.OrderedItems.Add(orderedItem);
                _context.SaveChanges();

                var oItem = from OrderItem in _context.OrderedItems
                            where OrderItem.OrderId == orderId
                            select new OrderedItem
                            {
                                Sub_Total = OrderItem.Sub_Total,
                            };
                Decimal TotalPrice = 0;
                foreach(var p in oItem)
                {
                    TotalPrice += ((Decimal)p.Sub_Total.Value);
                }

                return RedirectToAction("placeOrder", new { customerName = cName, customerGuid = cGuid.ToString(), oId = orderId, TotalPrice = TotalPrice });


            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult getUnits(Test obj)
        {

            var items = (from item in _context.Items

                         join ItemUnit in _context.UnitItems on item.Id equals ItemUnit.ItemId
                         select new Item()
                         {
                             Id = item.Id,
                             Name = item.Name,

                         });
            var uniqueItems = items.Distinct().ToList();

            var units = from unit in _context.Units
                        from UnitItem in _context.UnitItems
                        where unit.UnitId == UnitItem.UnitId && UnitItem.ItemId == obj.id
                        select new Unit()
                        {
                            UnitId = unit.UnitId,
                            UnitType = unit.UnitType
                        };

            //CustomerOrderVM customerOrderVM = new CustomerOrderVM()
            //{
            //    Items = uniqueItems,
            //    Units = units.ToList(),
            //    ItemId = obj.id,
            //    CustomerName = obj.cName,
            //    CustomerGuidKey = obj.cGuid,
            //    OrderId = obj.oid
            //};
            getUnitsVM getUnitsVM = new getUnitsVM()
            {
                Units = units.ToList(),


            };
            return PartialView("_getUnitsPV", getUnitsVM);
  

        }
        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order model)
        {
            var cName = model.CustomerName.ToString();
            string cGuid = Guid.NewGuid().ToString();
            model.OrderDate = DateTime.UtcNow;
            model.CustomerGuidKey = cGuid;
            model.TotalPrice = 0;
            _context.Orders.Add(model);
            _context.SaveChanges();
            int orderId = _context.Orders.Where(o => o.CustomerGuidKey == cGuid).Select(o=>o.OrderId).FirstOrDefault();
            //return redire
            //return RedirectToAction("Index");
            return RedirectToAction("placeOrder", new { customerName = cName, customerGuid = cGuid.ToString(), oId =orderId});
        }
    }

    public class Test
    {
        public int? id { get; set; }
        public int? oid { get; set; }
        public string? cName { get; set; }
        public string? cGuid { get; set; }
    }
}
