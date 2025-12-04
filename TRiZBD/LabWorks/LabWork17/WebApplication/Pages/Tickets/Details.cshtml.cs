using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Tickets
{
    public class DetailsModel : AuthPageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public DetailsModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsInRole("Билетер"))
                return RedirectToPage("Index");

            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket is not null)
            {
                Ticket = ticket;
                return Page();
            }

            return NotFound();
        }
    }
}
