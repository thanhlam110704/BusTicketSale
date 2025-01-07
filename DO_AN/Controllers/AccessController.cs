using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using DO_AN.ViewModel;
using Microsoft.EntityFrameworkCore;
using DO_AN.Services;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace DO_AN.Controllers
{
    public class AccessController : Controller
    {
        private readonly IWebHostEnvironment _env;

        private readonly DOAN_BoSungContext _context;

        public AccessController(DOAN_BoSungContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult notifiSendEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Accounts.AnyAsync(a => a.Email == registerVM.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại! Vui lòng sử dụng một Email khác.");
                    return View(registerVM);
                }

                if (registerVM.Phone.Length != 10 || !registerVM.Phone.All(char.IsDigit))
                {
                    ModelState.AddModelError("", "Số điện thoại phải có đủ 10 ký tự và không chứa ký tự đặc biệt.");
                    return View(registerVM);
                }

                var newAccount = new Account
                {
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                    Phone = registerVM.Phone,
                    Sex = registerVM.Sex,
                    DateOfBirth = registerVM.DateOfBirth,
                    IdRole = 2 // Giả sử ID vai trò cho người dùng thường
                };

                // Tạo token xác nhận email
                string token = Guid.NewGuid().ToString();

                // Lưu đối tượng dưới dạng JSON vào TempData
                TempData["NewAccount"] = JsonConvert.SerializeObject(newAccount);
                TempData["RegisterVM"] = JsonConvert.SerializeObject(registerVM);
                TempData["EmailVerificationToken"] = JsonConvert.SerializeObject(token);

                // Gửi email xác nhận
                string subject = "Xác thực tài khoản";
                string url = Url.Action("VerifyAccount", "Access", new { token }, Request.Scheme);
                //string body = $"<p><i>Web Sales Ticket xin chào,</i></p><p><i>Vui lòng nhấp vào liên kết dưới đây để xác thực tài khoản:</i></p><p><i>{url}</i></p>";
                //html xác nhận email
                string templatePath = Path.Combine(_env.WebRootPath, "ContentEmail", "confirmEmail.html");
                string body = System.IO.File.ReadAllText(templatePath);
                body = body.Replace("{{VerificationLink}}", url);

                SendEmail(registerVM.Email, subject, body);
                return RedirectToAction("notifiSendEmail", "Access");

                // return RedirectToAction("Login", "Access");
            }

            ViewBag.Status = 0; // Đăng ký thất bại
            return View(registerVM);
        }



        [HttpGet]
        public async Task<IActionResult> VerifyAccount(string token)
        {
            // Lấy dữ liệu từ TempData
            var accountJson = TempData["NewAccount"] as string;
            var registerVMJson = TempData["RegisterVM"] as string;
            var emailVerificationToken = TempData["EmailVerificationToken"] as string;



            var tokenverified = JsonConvert.DeserializeObject<string>(emailVerificationToken);

            if (!string.IsNullOrEmpty(accountJson) && !string.IsNullOrEmpty(registerVMJson) && token == tokenverified)
            {
                var account = JsonConvert.DeserializeObject<Account>(accountJson);
                var registerVM = JsonConvert.DeserializeObject<RegisterVM>(registerVMJson);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                var newCustomer = new Customer
                {
                    IdCus = account.IdAccount,
                    IdAccount = account.IdAccount,
                    FullName = registerVM.FullName
                };

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();

                // Xóa dữ liệu khỏi TempData
                TempData.Remove("NewAccount");
                TempData.Remove("RegisterVM");
                TempData.Remove("EmailVerificationToken");

                ViewBag.Message = "Xác thực thành công!";
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Message = "Xác thực không thành công!";
                ViewBag.Success = false;
            }
            return View();
        }


        private void SendEmail(string toEmail, string subject, string body)
        {
            var fromEmail = "nhattan425@gmail.com";
            var fromPassword = "chyx hwob foaq bjyr";
            var fromDisplayName = "Web Sales Ticket";

            using (var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            })
            {
                var fromAddress = new MailAddress(fromEmail, fromDisplayName);
                var toAddress = new MailAddress(toEmail);

                var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                smtpClient.Send(mailMessage);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account user)
        {
            var myUser = _context.Accounts.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                if (myUser.IdRole == 1)
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);
                    return RedirectToAction("DailyRevenueChart", "Sales", new { area = "Admin" });
                }
                else if (myUser.IdRole == 2)
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);

                    HttpContext.Session.SetInt32("UserID", myUser.IdAccount);
                    return RedirectToAction("TrangChu", "Home");
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

            return RedirectToAction("Login", "Access");
        }
    }
}
