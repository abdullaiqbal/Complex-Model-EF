using ComplexModel.Data;
using ComplexModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComplexModel.Areas.Administration.Controllers
{
    public class UnitController : Controller
    {
        private readonly DatabaseContext _context;
        public UnitController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Units.ToList();
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
            var item = _context.Units.Where(x => x.UnitId == id).FirstOrDefault();
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Units.Where(x => x.UnitId == id).FirstOrDefault();
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Units.Where(x => x.UnitId == id).FirstOrDefault();
            return View(item);
        }

        /// <summary>
        /// Post Secion of course
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Create(Unit model)
        {
            _context.Units.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Unit model)
        {
            _context.Units.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Unit model)
        {
            _context.Units.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
