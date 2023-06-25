using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace WordsTeacher.UI.Pages
{
    public class LogoutModel : PageModel
    {
		private SignInManager<IdentityUser> _signInManager;

		public LogoutModel(SignInManager<IdentityUser> signInManager)
		{
			_signInManager = signInManager;
		}

        public async Task OnGet()
        {
			if (User.Identity is not null && User.Identity.IsAuthenticated)
			{
				//await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

				//HttpContext.Response.Redirect("Index");


				//HttpContext.Response.Cookies.Delete("username");
				//await HttpContext.SignOutAsync();
				//HttpContext.Response.Redirect("Index");

				await _signInManager.SignOutAsync();
				HttpContext.Response.Redirect("Index");
			}
		}
    }
}
