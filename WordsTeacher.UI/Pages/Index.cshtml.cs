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
    public class IndexModel : PageModel
    {
		private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationContext _ctx;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        [DataType(DataType.Text)]
        [Required]
        public string Nick { get; set; } = null!;

        public string? MessageDisplayWhenNoUserNameSet { get; set; } = "";
        
        public IndexModel(ILogger<IndexModel> logger, 
            ApplicationContext ctx, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _ctx = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        
        public async Task OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                HttpContext.Response.Redirect("WordPage");
            }
        }


        public async Task OnPost()
        {
 
            var user = new IdentityUser(Nick);

            if (!_ctx.Users.Any())
            {
                await _userManager.CreateAsync(user, " ");
			}

            if (!_ctx.Users.Where(u => u.UserName == Nick).Any())
            {
                var us = await _userManager.CreateAsync(user, "111");
			}

            // default password is 111 for every user just to let them logins by only username
            var result = await _signInManager.PasswordSignInAsync(Nick, "111", true, false);

            if (result.Succeeded)
            {
                //HttpContext.Response.Cookies.Append("username", Nick);
                HttpContext.Response.Redirect("WordPage");
            }

            else HttpContext.Response.Redirect("Index");
		}
	}
}