using ComplexModel.Data;
using Microsoft.AspNetCore.Mvc;

namespace ComplexModel.Areas.Customer.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly DatabaseContext _context;
        public CustomerOrderController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            return View(items);
        }

        public IActionResult placeOrder()
        {

            return View();
        }
    }
}
