using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
    [Authorize]
    public class TrainModel : PageModel
    {
        private readonly ApplicationContext _ctx;
		private readonly SignInManager<IdentityUser> _signInManager;
		private IEnumerable<Word> _words;

        public string DisplayedWord { get; set; } = "";

        [BindProperty]
        public string TranslatedWord { get; set; } = "";

        public string PreviousWord { get; set; } = "";

        private int _wordIndex = 0;
        public int WordIndex { get => _wordIndex; set => _wordIndex = value; }

        public int PrevIndex { get; set; } = 0;

        public string CheckingResult { get; set; } = "false";

        public TrainModel(ApplicationContext ctx, SignInManager<IdentityUser> signInManager)
        {
            _ctx = ctx;
            _words = new List<Word>();
            _signInManager = signInManager;
        }

        public void OnGet(int result, string translated, string previous, int prevIndex)
        {
			var name = _signInManager.Context.User.Identity!.Name!;
			_words = _ctx.Words.Where(x => x.NickName == name);
            if (_words.Count() > 1)
            {
                // to prevent the same word be displayed in series

                var rnd = new Random(Guid.NewGuid().GetHashCode());
                _wordIndex = rnd.Next(0, _words.Count());
                while (prevIndex == _wordIndex)
                {
                    _wordIndex = rnd.Next(0, _words.Count());
                }
                PrevIndex = _wordIndex;

                var wordsAsArray = _words.ToArray();
                DisplayedWord = wordsAsArray[_wordIndex].Meaning;
                //
                if (translated != null && result >= 0)
                {
                    TranslatedWord = translated;
                    PreviousWord = previous;
					CheckingResult = CompareWordAndMeaning(word: translated, def: wordsAsArray[result].Definition);
                }
            }

            else
                CheckingResult = "NOT ENOUGH WORDS";

		}
        
        public void OnPost(int index, string previous, int prevIndex)
        {
            // calling OnGet with parameters from form
            OnGet(index, TranslatedWord, previous, prevIndex);
        }



        private static string CompareWordAndMeaning(string word, string def) =>
            word.Trim().ToLower() == def.Trim().ToLower() ? "true" : "false";

    }
}
