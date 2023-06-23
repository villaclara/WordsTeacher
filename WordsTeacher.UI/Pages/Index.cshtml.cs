using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        [BindProperty]
        [DataType(DataType.Text)]
        [Required]
        public string Nick { get; set; } = null!;

        public bool IsLoggedIn { get; set; } = false;

        public string? MessageDisplayWhenNoUserNameSet { get; set; } = "";
        public IndexModel(ILogger<IndexModel> logger, ApplicationContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
         
        }


        
        public async Task OnGet()
        {

            //if (HttpContext.Request.Cookies.ContainsKey("username"))
            //{
            //    Nick = HttpContext.Request.Cookies["username"];
            //    IsLoggedIn = true;
            //    //return RedirectToPage("WordPage");
            //    HttpContext.Response.Redirect("WordPage");
            //}


            //if (User.Identity!.IsAuthenticated)
            //{
            //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //    HttpContext.Response.Cookies.Delete("username");
            //    HttpContext.Response.Redirect("Index");
            //}


            if (HttpContext.Request.Cookies.ContainsKey("username"))
            {
                //Nick = HttpContext.Request.Cookies["username"]!;
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, Nick) };
                ClaimsIdentity claimsIdentity = new(claims, "Cookies");
                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
                HttpContext.Response.Redirect("WordPage");
            }

            //         if (User.Identity is not null && User.Identity.IsAuthenticated)
            //         {
            //	HttpContext.Response.Redirect("WordPage");

            //}

        }


        public async Task<IActionResult> OnPost()
        {

            //      HttpContext.Response.Cookies.Append("username", Nick);
            //      IsLoggedIn = true;
            ////      if (HttpContext.Request.Cookies["username"] != Nick)
            ////          HttpContext.Response.Cookies.Append("username", Nick);

            //return RedirectToPage("WordPage");

            if (ModelState.IsValid)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, Nick) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                HttpContext.Response.Cookies.Append("username", Nick);
                return RedirectToPage("WordPage");
            }

            else
            {
                return RedirectToPage("Index");
            }
        }

    }
}