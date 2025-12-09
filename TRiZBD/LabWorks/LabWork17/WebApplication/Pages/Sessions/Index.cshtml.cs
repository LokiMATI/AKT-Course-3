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
    public class IndexModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public IndexModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Session = await _context.Sessions
                .Include(s => s.Film)
                .Include(s => s.Hall).ToListAsync();
        }
    }
}
