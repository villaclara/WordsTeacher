using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
			
            if (HttpContext.Request.Cookies.ContainsKey("username"))
            {
                Nick = HttpContext.Request.Cookies["username"];
                IsLoggedIn = true;
                //return RedirectToPage("WordPage");
                HttpContext.Response.Redirect("WordPage");
            }

            

            //return null;

		}

        public async Task<IActionResult> OnPost()
        {

            HttpContext.Response.Cookies.Append("username", Nick);
            IsLoggedIn = true;
      //      if (HttpContext.Request.Cookies["username"] != Nick)
      //          HttpContext.Response.Cookies.Append("username", Nick);

		    return RedirectToPage("WordPage");
        }

        private IActionResult RedirToWordPage()
        {
            return RedirectToPage("WordPage");
        }
    }
}