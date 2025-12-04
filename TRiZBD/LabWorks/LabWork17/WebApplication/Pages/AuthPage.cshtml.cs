using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages;

public class AuthPageModel : PageModel
{
    public string? UserRole => HttpContext.Session.GetString("Role");
    public bool IsAdmin => UserRole == "Администратор";

    protected IActionResult? HasRole()
    {
        if (string.IsNullOrEmpty(UserRole))
            return RedirectToPage("/Login");
        return null;
    }

    protected bool IsInRole(string role) 
        => role == HttpContext.Session.GetString("Role");

    protected IActionResult? CanEdit()
    {
        if (IsAdmin)
            return null;
        return RedirectToPage("Index"); ;
    }
}
