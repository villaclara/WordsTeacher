using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.Application.Users;
using WordsTeacher.Application.Words;
using WordsTeacher.DB;

namespace WordsTeacher.UI.Pages
{
    public class AdminModel : PageModel
    {
        private ApplicationContext _ctx;

        public IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public AdminModel(ApplicationContext ctx)
        {
            _ctx = ctx;
        }



        public void OnGet()
        {
            Users = _ctx.Users;
        }

		public async Task<IActionResult> OnPostDeleteAsync(string user)
		{
            await new DeleteUser(_ctx).Do(user);
			return RedirectToPage("Admin");
		}
	}
}
