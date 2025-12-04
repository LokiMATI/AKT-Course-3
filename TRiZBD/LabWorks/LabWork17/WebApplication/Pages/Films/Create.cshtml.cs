using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Films
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

            return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Films.Add(Film);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
