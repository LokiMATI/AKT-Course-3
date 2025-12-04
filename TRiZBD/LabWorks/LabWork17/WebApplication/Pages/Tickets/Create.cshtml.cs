using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Tickets
{
    public class CreateModel : AuthPageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public CreateModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (CanEdit() is IActionResult action)
                return action;

            ViewData["SessionId"] = new SelectList(_context.Sessions, "SessionId", "SessionId");
        ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tickets.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
