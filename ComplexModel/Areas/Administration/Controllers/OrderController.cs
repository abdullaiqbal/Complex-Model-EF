using ComplexModel.Data;
using ComplexModel.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            return View(item);
        }

        /// <summary>
        /// Post Secion of course
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Create(Order model)
        {
            _context.Orders.Add(model);
            _context.SaveChanges();
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
