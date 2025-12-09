using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public CreateModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "Title");
        ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "CinemaHall");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Session.Film");
            ModelState.Remove("Session.Hall");
            ModelState.Remove("Session.Tickets");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
