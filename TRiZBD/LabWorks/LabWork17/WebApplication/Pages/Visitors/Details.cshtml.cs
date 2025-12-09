using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Visitors
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public DetailsModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public Visitor Visitor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors.FirstOrDefaultAsync(m => m.VisitorId == id);

            if (visitor is not null)
            {
                Visitor = visitor;

                return Page();
            }

            return NotFound();
        }
    }
}
