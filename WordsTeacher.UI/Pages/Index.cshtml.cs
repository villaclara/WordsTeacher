using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.DB;

namespace WordsTeacher.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationContext _ctx;

        public string Nick { get; set; } = "";

        private IEnumerable<string> allNickNames { get; set; }

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
                
				//return RedirectToPage("WordPage");
			}

            //return null;

		}

        public async Task<IActionResult> OnPost()
        {
            
            
            
            if (HttpContext.Request.Cookies["username"] != Nick)
                HttpContext.Response.Cookies.Append("username", Nick);

		    return RedirectToPage("WordPage");
        }

        private IActionResult RedirToWordPage()
        {
            return RedirectToPage("WordPage");
        }
    }
}