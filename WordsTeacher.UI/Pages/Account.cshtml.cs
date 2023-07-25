using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.DB;

namespace WordsTeacher.UI.Pages
{
    public class AccountModel : PageModel
    {
        private readonly ApplicationContext _context;
        public int WordsPerUser { get; set; }

        public AccountModel(ApplicationContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            WordsPerUser = _context.Words.Where(w => w.NickName == HttpContext.User.Identity!.Name).Count();
        }

        public void OnPostChangePassword()
        {

        }

	}
}
