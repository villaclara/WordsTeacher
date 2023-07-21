using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.Application;
using WordsTeacher.Application.Words;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
	public class WordPageModel : PageModel
    {
        private readonly ApplicationContext _ctx;

		private readonly SignInManager<IdentityUser> _signInManager;

		public WordPageModel(ApplicationContext ctx, SignInManager<IdentityUser> signInManager)
        {
            _ctx = ctx;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Word OneWord { get; set; } = null!;

        public IEnumerable<Word> Words { get; set; } = Enumerable.Empty<Word>();

        public void OnGet()
        {
            
            var name = _signInManager.Context.User.Identity?.Name;

            if (name is not null)
                Words = new GetWords(_ctx).Do(name);

            else HttpContext.Response.Redirect("Login");
		}

        // default post with method=post button=submit
        public async Task<IActionResult> OnPostAsync()
        {
            await new CreateWord(_ctx).Do(new Word()
            {
                //Id = _ctx.Words.Count() + 1, - automatically increments in the db
                Definition = OneWord.Definition.Trim(),
                Meaning = OneWord.Meaning.Trim(),
				NickName = _signInManager.Context.User.Identity!.Name!
			});

            return RedirectToPage("WordPage");
        }

        // asp-page-handler is used in form with 'delete' value
        public async Task<IActionResult> OnPostDeleteAsync(string word, string meaning)
        {
            var nick = User.Identity!.Name;
            await new DeleteWord(_ctx).DoAsync(word, meaning, nick!);
            return RedirectToPage("WordPage");
		}
    }
}
