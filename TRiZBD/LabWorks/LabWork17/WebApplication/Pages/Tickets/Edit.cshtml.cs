using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Pages.Tickets
{
    public class EditModel : AuthPageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public EditModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (CanEdit() is IActionResult action)
                return action;

            if (id == null)
            {
                return NotFound();
            }

            var ticket =  await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }
            Ticket = ticket;
           ViewData["SessionId"] = new SelectList(_context.Sessions, "SessionId", "SessionId");
           ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(Ticket.TicketId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
