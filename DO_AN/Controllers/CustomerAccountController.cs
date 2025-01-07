using DO_AN.Models;
using DO_AN.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Controllers
{
    public class CustomerAccountController : Controller
    {
        private readonly DOAN_BoSungContext _context;

        public CustomerAccountController(DOAN_BoSungContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ManagerAccountCustomer(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            var customer = await _context.Customers
                .Include(c => c.IdAccountNavigation)
                .FirstOrDefaultAsync(c => c.IdAccount == id);

            if (account == null || customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerAccountViewModel
            {
                Account = account,
                Customer = customer
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManagerAccountCustomer(int id, string fullName, string email, string phone, bool sex, DateTime dateOfBirth)
        {
            var account = await _context.Accounts.FindAsync(id);
            var customer = await _context.Customers
                .Include(c => c.IdAccountNavigation)
                .FirstOrDefaultAsync(c => c.IdAccount == id);

            if (account == null || customer == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin Account
            
            account.DateOfBirth = dateOfBirth;
            account.Phone = phone;
            account.Sex = sex;

            // Cập nhật thông tin Customer
            customer.FullName = fullName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManagerAccountCustomer), new { id });
        }
    }

}
