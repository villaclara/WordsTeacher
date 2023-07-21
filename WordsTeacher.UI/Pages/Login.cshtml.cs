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
		[DataType(DataType.Text)]
		[Required]
		public string Nick { get; set; } = null!;

		[BindProperty]
		public string Password { get; set; } = null!;

		public string? MessageDisplayWhenNoUserNameSet { get; set; } = "";

		public LoginModel(
			ApplicationContext ctx,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			_ctx = ctx;
			_userManager = userManager;
			_signInManager = signInManager;
		}



		public void OnGet()
		{
			
		}


		public async Task OnPostLogin()
		{

			var user = new IdentityUser(Nick);

			if (!_ctx.Users.Any())
			{
				await _userManager.CreateAsync(user, Password);
			}

			if (!_ctx.Users.Where(u => u.UserName == Nick).Any())
			{
				var us = await _userManager.CreateAsync(user, Password);
			}

			// default password is 111 for every user just to let them logins by only username
			var result = await _signInManager.PasswordSignInAsync(Nick, Password, true, false);

			if (result.Succeeded)
			{
				//HttpContext.Response.Cookies.Append("username", Nick);
				HttpContext.Response.Redirect("WordPage");
			}

			else HttpContext.Response.Redirect("Index");
		}

		public async Task OnPostRegister()
		{

		}
	}
}