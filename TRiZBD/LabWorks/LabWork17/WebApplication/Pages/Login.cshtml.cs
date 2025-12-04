using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contexts;
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Pages
{
    public class LoginModel : PageModel
    {
        private readonly WebApplication.Contexts.AppDbContext _context;

        public LoginModel(WebApplication.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.CinemaUserRoles, "RoleId", "RoleId");
            return Page();
        }

        [BindProperty]
        public UserDto User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            var user = _context.CinemaUsers
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == User.Login);

            using var sha256 = SHA256.Create();

            var passwordHash = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(User.Password)));

            if (user is null || user.PasswordHash != passwordHash)
                return Page();

            HttpContext.Session.SetString("Login", user.Login);
            HttpContext.Session.SetString("Role", user.Role.Title);
            return RedirectToPage("/Films/Index");
        }

        public async Task<IActionResult> OnPostGuestAsync()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("Role", "Гость");
            return RedirectToPage("/Films/Index");
        }
    }
}
