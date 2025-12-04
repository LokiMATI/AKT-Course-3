using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lection1202.Contexts;
using Lection1202.Models;

namespace Lection1202.Pages.Categories
{
    public class IndexModel : AccessPageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public IndexModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HasRole() is IActionResult action)
                return action;

            Category = await _context.Categories.ToListAsync();
            return Page();
        }
    }
}
