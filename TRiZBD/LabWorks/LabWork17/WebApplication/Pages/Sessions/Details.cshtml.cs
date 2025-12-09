using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public DetailsModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FirstOrDefaultAsync(m => m.SessionId == id);

            if (session is not null)
            {
                Session = session;

                return Page();
            }

            return NotFound();
        }
    }
}
