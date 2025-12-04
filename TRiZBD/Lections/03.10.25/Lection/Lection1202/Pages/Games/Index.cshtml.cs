using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lection1202.Contexts;
using Lection1202.Models;

namespace Lection1202.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public IndexModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string GameName { get; set; }

        public async Task OnGetAsync()
        {
            Game = await _context.Games
                .Include(g => g.Category).ToListAsync();
        }
    }
}
