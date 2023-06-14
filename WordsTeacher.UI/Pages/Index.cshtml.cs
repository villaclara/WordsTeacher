using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WordsTeacher.DB;

namespace WordsTeacher.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationContext _ctx;

        [BindProperty]
        public string Nick { get; set; } = "";

        private IEnumerable<string> allNickNames { get; set; }

        public bool IsLoggedIn { get; set; } = false;

        public IndexModel(ILogger<IndexModel> logger, ApplicationContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
         
        }


        
        public async void OnGet()
        {

            //if (HttpContext.Request.Cookies.ContainsKey("username"))
            //{
            //    Nick = HttpContext.Request.Cookies["username"];
            //    IsLoggedIn = true;
            //    //return RedirectToPage("WordPage");
            //    HttpContext.Response.Redirect("WordPage");
            //}

            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Cookies.Delete("username");
                HttpContext.Response.Redirect("Index");
            }
          

		}

        public async Task<IActionResult> OnPost()
        {

      //      HttpContext.Response.Cookies.Append("username", Nick);
      //      IsLoggedIn = true;
      ////      if (HttpContext.Request.Cookies["username"] != Nick)
      ////          HttpContext.Response.Cookies.Append("username", Nick);

		    //return RedirectToPage("WordPage");


            var claims = new List<Claim> { new Claim(ClaimTypes.Name, Nick) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            HttpContext.Response.Cookies.Append("username", Nick);
            return RedirectToPage("WordPage");
        }

        private IActionResult RedirToWordPage()
        {
            return RedirectToPage("WordPage");
        }
    }
}