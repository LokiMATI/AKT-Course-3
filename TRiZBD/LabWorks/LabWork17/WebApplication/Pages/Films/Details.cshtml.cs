using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Films
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public DetailsModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FirstOrDefaultAsync(m => m.FilmId == id);

            if (film is not null)
            {
                Film = film;

                return Page();
            }

            return NotFound();
        }
    }
}
