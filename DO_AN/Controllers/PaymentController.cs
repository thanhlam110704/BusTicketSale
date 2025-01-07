using Microsoft.AspNetCore.Mvc;

namespace DO_AN.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
