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
        
        public string EN_MeaningPreviousWord { get; set; } = "";
        public string UA_DefinitionPreviousWord { get; set; } = "";


        public int CurrentWordIndex { get; set; }

        public string CheckingResult { get; set; } = "false";

        public TrainModel(ApplicationContext ctx, SignInManager<IdentityUser> signInManager)
        {
            _ctx = ctx;
            _words = new List<Word>();
            _signInManager = signInManager;
        }

        public void OnGet(int prevWordIndex, string translatedfromUser)
        {
			var name = _signInManager.Context.User.Identity!.Name!;
			_words = _ctx.Words.Where(x => x.NickName == name);
            
            
            if (_words.Count() > 1)
            {
                // to prevent the same word be displayed in series
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                CurrentWordIndex = rnd.Next(0, _words.Count());
                while (prevWordIndex == CurrentWordIndex)
                {
                    CurrentWordIndex = rnd.Next(0, _words.Count());
                }


                var wordsAsArray = _words.ToArray();
                DisplayedWordToTranslate = wordsAsArray[CurrentWordIndex].Meaning;
                

                // checking result from previous translation
                if (translatedfromUser != null && prevWordIndex >= 0)
                {
                    // these assignments are needed to display the values in the html page
                    TranslatedWordFromUser = translatedfromUser;
                    UA_DefinitionPreviousWord = wordsAsArray[prevWordIndex].Meaning;
					EN_MeaningPreviousWord = wordsAsArray[prevWordIndex].Definition;

					CheckingResult = CompareWordAndMeaning(word: TranslatedWordFromUser, def: EN_MeaningPreviousWord);
                }
            }

            else
                CheckingResult = "NOT ENOUGH WORDS";

		}
        
        public void OnPost(int index)
        {
            // calling OnGet with parameters from form
            OnGet(index, TranslatedWordFromUser);
        }



        private static string CompareWordAndMeaning(string word, string def) =>
            word.Trim().ToLower() == def.Trim().ToLower() ? "true" : "false";

    }
}
