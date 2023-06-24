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

        private UserManager<IdentityUser> _userManager;

        private SignInManager<IdentityUser> _signInManager;

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
            //if (User.Identity is not null && User.Identity.IsAuthenticated)
            //{
            //    if (HttpContext.Request.Cookies.ContainsKey("username"))
            //    {
            //        Nick = HttpContext.Request.Cookies["username"]!;
            //    }
            //    HttpContext.Response.Redirect("WordPage");
            //}

            if (_signInManager.IsSignedIn(User))
            {
                HttpContext.Response.Redirect("WordPage");
            }

        }


        public async Task OnPost()
        {

            //         var claims = new List<Claim>()
            //         {
            //             new Claim(ClaimTypes.Name, Nick)
            //         };
            //         var identity = new ClaimsIdentity(claims, "Cookies");
            //         HttpContext.Response.Cookies.Append("username", Nick);
            //         await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
            //         {
            //             IsPersistent = true,
            //             RedirectUri = "WordPage",
            //         });
            //HttpContext.Response.Redirect("WordPage");  

            var user = new IdentityUser(Nick);

            if (!_ctx.Users.Any())
            {
                await _userManager.CreateAsync(user, " ");
			}

            if (!_ctx.Users.Where(u => u.UserName == Nick).Any())
            {
                var us = await _userManager.CreateAsync(user, "111");
			}




            var result = await _signInManager.PasswordSignInAsync(Nick, "111", false, false);

            if (result.Succeeded)
            {
                HttpContext.Response.Cookies.Append("username", Nick);
                HttpContext.Response.Redirect("WordPage");
            }

            else HttpContext.Response.Redirect("Index");
		}
	}
}