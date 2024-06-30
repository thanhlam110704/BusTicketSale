using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DO_AN.Models;
namespace DO_AN.Pages.AccountDt
{
    public class IndexModel : PageModel
    {
        public Account? Account { get; set; }
        private DOANContext context { get; set; }   
        public IndexModel(DOANContext _context)
        {
            context = _context;
        }
        public async Task OnGetAsync(int id = 3)
        {
            Account = await context.Accounts.FindAsync(id);
        }

    }
}
