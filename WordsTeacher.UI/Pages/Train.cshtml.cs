using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        private IEnumerable<Word> _words;

        public string DisplayedWord { get; set; } = "";

        [BindProperty]
        public string TranslatedWord { get; set; } = "";

        private int _wordIndex = 0;

        public int WordIndex { get => _wordIndex; set => _wordIndex = value; }

        public string CheckingResult { get; set; } = "false";

        public TrainModel(ApplicationContext ctx)
        {
            _ctx = ctx;
            _words = new List<Word>();
        }

        public void OnGet(int result, string translated)
        {
			_words = _ctx.Words.Where(x => x.NickName == HttpContext.Request.Cookies["username"]);
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            _wordIndex = rnd.Next(0, _words.Count());
            var arr = _words.ToArray();
            DisplayedWord = arr[_wordIndex].Meaning;
            TranslatedWord = translated;

            if (translated != null && result != 0)
            {
                if (translated.ToLower() == arr[result].Definition.ToLower())
                {
                    CheckingResult = "true";

                }
                else
                {
                    CheckingResult = "false";
                }

            }
            //if (result == true )
            //{
            //    CheckingResult = "true";
            //}
            //else 
            //{ 
            //    CheckingResult = "false";  
            //}

            
		}

        
        public void OnPost(int index)
        {
			_words = _ctx.Words.Where(x => x.NickName == HttpContext.Request.Cookies["username"]);
            var arr = _words.ToArray();
            OnGet(index, TranslatedWord);
   //         if (TranslatedWord.ToLower() == arr[index].Definition.ToLower())
   //         {
			//	OnGet(true);
			//}
   //         else {
			//	OnGet(false); 
   //         }
            
            
        }
    }
}
