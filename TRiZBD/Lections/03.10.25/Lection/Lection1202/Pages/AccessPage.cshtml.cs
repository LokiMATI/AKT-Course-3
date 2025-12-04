using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lection1202.Pages
{
    public class AccessPageModel : PageModel
    {
        public string? Role => HttpContext.Session.GetString("Role");

        public bool IsAdmin => Role == "Admin";

        protected IActionResult? HasRole()
        {
            if (string.IsNullOrWhiteSpace(Role))
                return RedirectToPage("/Login");

            return null;
        }
    }
}
