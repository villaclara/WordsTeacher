using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.Application.Users;
using WordsTeacher.Application.Words;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
    [Authorize]
    public class AdminModel : PageModel
    {
        private readonly ApplicationContext _ctx;

        public IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public IEnumerable<UserModelForAdmin> UsersForAdmin { get; set; } = new List<UserModelForAdmin>();

        public AdminModel(ApplicationContext ctx)
        {
            _ctx = ctx;
        }



        public void OnGet()
        {
            Users = _ctx.Users;

            UsersForAdmin = new List<UserModelForAdmin>();


        }

		public async Task<IActionResult> OnPostDeleteAsync(string user)
		{
            await new DeleteUser(_ctx).Do(user);
			return RedirectToPage("Admin");
		}

        public class UserModelForAdmin
        {
            public IdentityUser identity { get; set; }

            public IEnumerable<Word> words { get; set; }
        }
	}
}
