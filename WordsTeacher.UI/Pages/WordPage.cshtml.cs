using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.Application;
using WordsTeacher.Application.Words;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
	[Authorize]
	public class WordPageModel : PageModel
    {
        private ApplicationContext _ctx;

        

        public WordPageModel(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public Word OneWord { get; set; } = null!;

        public IEnumerable<Word> Words { get; set; } = Enumerable.Empty<Word>();

        public void OnGet()
        {
            Words = new GetWords(_ctx).Do(HttpContext.Request.Cookies["username"]!);
        }

        // default post with method=post button=submit
        public async Task<IActionResult> OnPostAsync()
        {
            await new CreateWord(_ctx).Do(new Word()
            {
                NickName = HttpContext.Request.Cookies["username"]!,
                //Id = _ctx.Words.Count() + 1,
                Definition = OneWord.Definition,
                Meaning = OneWord.Meaning
            });

            return RedirectToPage("WordPage");
        }

        // asp-page-handler is used in form with 'delete' value
        public async Task<IActionResult> OnPostDeleteAsync(string word, string meaning)
        {
            var nick = HttpContext.Request.Cookies["username"];
            await new DeleteWord(_ctx).DoAsync(word, meaning, nick!);
            return RedirectToPage("WordPage");
		}
    }
}
