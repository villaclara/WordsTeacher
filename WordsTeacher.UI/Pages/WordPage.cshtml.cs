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
        private ApplicationContext _ctx;

        

        public WordPageModel(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public Word OneWord { get; set; }

        public IEnumerable<Word> Words { get; set; }

        public void OnGet()
        {
            Words = new GetWords(_ctx).Do(HttpContext.Request.Cookies["username"]!);
        }

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
    }
}
