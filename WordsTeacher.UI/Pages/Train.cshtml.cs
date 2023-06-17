using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
    public class TrainModel : PageModel
    {
        private readonly ApplicationContext _ctx;

        private IEnumerable<Word> _words;

        public string DisplayedWord { get; set; } = "";

        [BindProperty]
        public string Translated { get; set; } = "";

        private int _wordIndex = 0;

        public string CheckingResult { get; set; } = "false";

        public TrainModel(ApplicationContext ctx)
        {
            _ctx = ctx;
            _words = new List<Word>();
        }

        public void OnGet()
        {
            _words = _ctx.Words.Where(x => x.NickName == HttpContext.Request.Cookies["username"]);

            var rnd = new Random(Guid.NewGuid().GetHashCode());
            _wordIndex = rnd.Next(0, _words.Count());

            var arr = _words.ToArray();

            DisplayedWord = arr[_wordIndex].Definition;

            CheckingResult = "false";
        }

        
        public void OnPost()
        {
            _words = _ctx.Words.Where(x => x.NickName == HttpContext.Request.Cookies["username"]);

            var arr = _words.ToArray();
            if (Translated.ToLower() == arr[_wordIndex].Meaning.ToLower())
            {
                CheckingResult = "true";
            }
        }
    }
}
