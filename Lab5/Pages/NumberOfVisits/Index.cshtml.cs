using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lab5.Data;
using Lab5.Models;

namespace Lab5.Pages.NumberOfVisits
{
    public class IndexModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public IndexModel(BankDepositContext db)
        {
            _context = db;
        }
        public List<Visit> Visits { get; set; }

        public void OnGet()
        {
            Visits = _context.Visits
                .Include(p => p.Person)
                .ToList();
        }
    }
}