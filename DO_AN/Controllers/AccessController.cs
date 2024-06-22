using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using DO_AN.ViewModel;

namespace DO_AN.Controllers
{
    public class AccessController : Controller
    {
        
        private readonly DOANContext context;
        public AccessController(DOANContext context)
        {
            this.context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM registerVM)
        {
            //try
            //{
            //    DOANContext daContext = new DOANContext();
            //    var user = new Account()
            //    {

            //        Email = registerVM.Email,
            //        Password = registerVM.Password,
            //        Phone = registerVM.Phone,
            //        DateOfBirth = registerVM.DateOfBirth,
            //        Sex = registerVM.Sex,
            //        IdRole = 2
            //    };
            //    daContext.Accounts.Add(user);
            //    daContext.SaveChanges();
            //    ViewBag.Status = 1;
            //}
            //catch
            //{
            //    ViewBag.Status = 0;
            //}
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account user)
        {
            var myUser = context.Accounts.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if(myUser != null)
            {
                HttpContext.Session.SetString("UserSession",myUser.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Login Failed...";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserSession");

            return RedirectToAction("Login","Access");
        }
    }
}
