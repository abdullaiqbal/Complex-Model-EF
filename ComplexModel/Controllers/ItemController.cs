using ComplexModel.Data;
using ComplexModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplexModel.Controllers
{
    public class ItemController : Controller
    {
        private readonly DatabaseContext _context;
        public ItemController(DatabaseContext context)
        {
            _context= context;
        }
        [HttpGet]
        public IActionResult Index()
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
        public IActionResult Details(int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            return View(item);
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

        [HttpPost]
        public IActionResult Create(Item model)
        {
            _context.Items.Add(model);
            _context.SaveChanges();
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
