using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
    public class AccountModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly string? _username;
		private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

		public int WordsPerUser { get; set; }

        public string IsPwdChanged { get; set; } = "";

        [BindProperty]
        public ChangePassword ChangePwd { get; set; } = new ChangePassword();

        public AccountModel(ApplicationContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _username = _signInManager.Context.User.Identity?.Name ?? null;
            WordsPerUser = _context.Words.Where(w => w.NickName == _username).Count();
        }

        public void OnGet()
        {
            if (_username is not null)
            {


            }

            else
                HttpContext.Response.Redirect("Login");

        }

        public async Task OnPostChangePassword()
        {

            if (!IsMatchPasswords(ChangePwd.NewPwd, ChangePwd.ConfirmedNewPwd))
            {
                IsPwdChanged = "New Passwords do not match.";
            }

            else
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _username);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, ChangePwd.OldPwd, ChangePwd.NewPwd);

                    if (result.Succeeded)
                    {
                        IsPwdChanged = "Password Changed.";
                    }
                    else
                        IsPwdChanged = "Old Password is incorrect or New is too short.";
                }

            }
            
        }

        private static bool IsMatchPasswords (string p1, string p2) => p1 == p2;

	}
}
