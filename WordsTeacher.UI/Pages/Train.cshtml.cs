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

        public string DisplayedWordToTranslate { get; set; } = "";

        [BindProperty]
        public string TranslatedWordFromUser { get; set; } = "";
        
        public string MeaningPreviousWord { get; set; } = "";
        public string DefinitionPreviousWord { get; set; } = "";

        private int _wordIndex = 0;
        public int CurrentWordIndex { get => _wordIndex; set => _wordIndex = value; }

        public int PrevWordIndex { get; set; } = 0;

        public string CheckingResult { get; set; } = "false";

        public TrainModel(ApplicationContext ctx, SignInManager<IdentityUser> signInManager)
        {
            _ctx = ctx;
            _words = new List<Word>();
            _signInManager = signInManager;
        }

        public void OnGet(int index, string translatedfromUser, int previousIndex)
        {
			var name = _signInManager.Context.User.Identity!.Name!;
			_words = _ctx.Words.Where(x => x.NickName == name);
            if (_words.Count() > 1)
            {
                // to prevent the same word be displayed in series

                var rnd = new Random(Guid.NewGuid().GetHashCode());
                _wordIndex = rnd.Next(0, _words.Count());
                while (previousIndex == _wordIndex)
                {
                    _wordIndex = rnd.Next(0, _words.Count());
                }
                PrevWordIndex = _wordIndex;
                var wordsAsArray = _words.ToArray();
                DisplayedWordToTranslate = wordsAsArray[_wordIndex].Meaning;
                MeaningPreviousWord = wordsAsArray[previousIndex].Definition;
                //
                if (translatedfromUser != null && index >= 0)
                {
                    TranslatedWordFromUser = translatedfromUser;
                    DefinitionPreviousWord = wordsAsArray[previousIndex].Meaning;
					CheckingResult = CompareWordAndMeaning(word: translatedfromUser, def: wordsAsArray[index].Definition);
                }
            }

            else
                CheckingResult = "NOT ENOUGH WORDS";

		}
        
        public void OnPost(int index, int prevIndex)
        {
            // calling OnGet with parameters from form
            OnGet(index, TranslatedWordFromUser, prevIndex);
        }



        private static string CompareWordAndMeaning(string word, string def) =>
            word.Trim().ToLower() == def.Trim().ToLower() ? "true" : "false";

    }
}
