using Microsoft.AspNetCore.Mvc;

namespace DO_AN.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
