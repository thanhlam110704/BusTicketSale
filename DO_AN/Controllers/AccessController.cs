using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using DO_AN.ViewModel;
using Microsoft.EntityFrameworkCore;

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
            DOANContext context = new DOANContext ();
            if (ModelState.IsValid)
            {
                
                    var empdata = new Account()
                    {
                        Email = registerVM.Email,
                        Password = registerVM.Password,
                        Phone = registerVM.Phone,
                        Sex = registerVM.Sex,
                        DateOfBirth = registerVM.DateOfBirth,
                        IdRole = 2
                    };
                
                
                context.Accounts.Add(empdata);
                context.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Status = 0;
            }
            
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
                if(user.IdRole == 1)
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);
                    return RedirectToAction("Register", "Access");
                }
                else
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);
                    return RedirectToAction("Index", "Home");
                }
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
