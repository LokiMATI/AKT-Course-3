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

namespace WebApplication.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public IndexModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? FilmTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Hall { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["Halls"] = new SelectList(_context.Halls, "HallId", "Информация");
            var sessions = _context.Sessions
                .Include(s => s.Film)
                .Include(s => s.Hall)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(FilmTitle))
                sessions = sessions.Where(s => s.Film.Title.Contains(FilmTitle));

            sessions = SortColumn switch
            {
                "price" => sessions.OrderBy(s => s.Price),
                "price_desc" => sessions.OrderByDescending(s => s.Price),
                _ => sessions
            };

            if (!string.IsNullOrWhiteSpace(Hall) && int.TryParse(Hall, out int hallId))
                sessions = sessions.Where(s => s.HallId == hallId);

            Session = await sessions.ToListAsync();
        }
    }
}
