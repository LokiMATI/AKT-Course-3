using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Tickets
{
    public class IndexModel : AuthPageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public IndexModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!(IsInRole("Билетер") || IsInRole("Администратор")))
                return RedirectToPage("Index");

            Ticket = await _context.Tickets
                .Include(t => t.Session)
                .Include(t => t.Visitor).ToListAsync();

            return Page();
        }
    }
}
