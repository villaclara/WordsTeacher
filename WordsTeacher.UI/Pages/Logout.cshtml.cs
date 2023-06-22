using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WordsTeacher.UI.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
			if (User.Identity!.IsAuthenticated)
			{
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				HttpContext.Response.Cookies.Delete("username");
				HttpContext.Response.Redirect("Index");
			}
		}
    }
}
