using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Claims;
using WordsTeacher.DB;

namespace WordsTeacher.UI.Pages
{
	public class LoginModel : PageModel
	{
		private readonly ApplicationContext _ctx;

		private readonly UserManager<IdentityUser> _userManager;

		private readonly SignInManager<IdentityUser> _signInManager;

		[BindProperty]
		public string Nick { get; set; } = null!;

		[BindProperty]
		public string Password { get; set; } = null!;

		public string? ErrorLoginMessage { get; set; } = "";

		public LoginModel(
			ApplicationContext ctx,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			_ctx = ctx;
			_userManager = userManager;
			_signInManager = signInManager;
		}



		public void OnGet(bool LoggedError)
		{
			if (LoggedError)
				ErrorLoginMessage = "Password is incorrect or Username is already in use by someone another.";
		}


		public async Task OnPostLogin()
		{

			var user = new IdentityUser(Nick);

			// if no users
			if (!_ctx.Users.Any())
			{
				await _userManager.CreateAsync(user, Password);
			}

			// if no user is found with that nickname
			if (!_ctx.Users.Where(u => u.UserName == Nick).Any())
			{
				await _userManager.CreateAsync(user, Password);
			}

			var result = await _signInManager.PasswordSignInAsync(Nick, Password, true, false);

			if (result.Succeeded)
			{
				//HttpContext.Response.Cookies.Append("username", Nick);
				HttpContext.Response.Redirect("WordPage");
			}

			else
			{
				OnGet(true);
			}
		}

	}
}