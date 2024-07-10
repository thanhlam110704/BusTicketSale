using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using DO_AN.ViewModel;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;


namespace DO_AN.Controllers
{
    public class AccessController : Controller
    {
        
        private readonly DOANContext context;
        //private readonly IEmailSender _emailSender;
        public AccessController(DOANContext context /*IEmailSender emailSender*/)
        {
            this.context = context;
            //_emailSender = emailSender;

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
                var newCustomer = new Customer
                {
                    IdAccount = empdata.IdAccount,
                    FullName = registerVM.FullName
                };
                context.Customers.Add(newCustomer);
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
                    HttpContext.Session.SetInt32("UserID", myUser.IdAccount);
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
        public IActionResult AccountInfo()
        {
            return View();
        }



        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var account = await context.Accounts.SingleOrDefaultAsync(a => a.Email == model.Email);
        //    if (account == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "No account found with that email address.");
        //        return View(model);
        //    }

        //    var token = Guid.NewGuid().ToString(); // Generate a reset token
        //    var resetLink = Url.Action("ResetPassword", "Account", new { token }, Request.Scheme);

        //    var message = $"Reset your password by clicking <a href='{resetLink}'>here</a>";
        //    await _emailSender.SendEmailAsync(model.Email, "Reset Your Password", message);

        //    TempData["Message"] = "Check your email for the password reset link.";
        //    return RedirectToAction("ForgotPassword");
        //}

        //[HttpGet]
        //public IActionResult ResetPassword(string token)
        //{
        //    return View(new ResetPasswordViewModel { Token = token });
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // Here you should verify the token and reset the password
        //    var account = await context.Accounts.SingleOrDefaultAsync(a => a.Email == model.Email);
        //    if (account == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid email address.");
        //        return View(model);
        //    }

        //    account.Password = HashPassword(model.NewPassword); // Ensure you hash the password
        //    await context.SaveChangesAsync();

        //    TempData["Message"] = "Your password has been reset.";
        //    return RedirectToAction("Login");
        //}

        //private string HashPassword(string password)
        //{
        //    // Implement secure hashing algorithm
        //    return BCrypt.Net.BCrypt.HashPassword(password); // Example using BCrypt
        //}

    }
}
