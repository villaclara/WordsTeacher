using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Runtime.CompilerServices;
using WordsTeacher.Application;
using WordsTeacher.Application.Words;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.UI.Pages
{
	public enum WordsOrder : byte
	{
		NewestToOldest = 1,
		OldestToNewest = 2
	}

	public class WordPageModel : PageModel
	{
		private readonly ApplicationContext _ctx;

		private readonly SignInManager<IdentityUser> _signInManager;

		private readonly string? _username;

		public WordsOrder OrderWords = WordsOrder.NewestToOldest;

		public string ErrorAddingMessage { get; set; } = "";

		[BindProperty]
		public Word OneWord { get; set; } = null!;

		public IEnumerable<Word> Words { get; set; } = Enumerable.Empty<Word>();

		

		public WordPageModel(ApplicationContext ctx, SignInManager<IdentityUser> signInManager)
		{
			_ctx = ctx;
			_signInManager = signInManager;
			_username = _signInManager.Context.User.Identity?.Name;
			//if (_username is not null)
			//{
			//	Words = new GetWords(_ctx).Do(_username).Reverse();
			//}
		}

		

		public void OnGet()
		{
			if (_username is not null)
			{
				Words = new GetWords(_ctx).Do(_username);

				if (OrderWords == WordsOrder.NewestToOldest)
				{ Words = Words.Reverse(); }

				ErrorAddingMessage = HttpContext.Request.Cookies["error"] ?? "";
			}

			else HttpContext.Response.Redirect("Login");
		}

		// default post with method=post button=submit
		public async Task OnPostAsync()
		{
			bool isWordAdded = await new CreateWord(_ctx).Do(new Word()
			{
				//Id = _ctx.Words.Count() + 1, - automatically increments in the db
				Definition = OneWord.Definition.Trim(),
				Meaning = OneWord.Meaning.Trim(),
				NickName = _username!
			});

			if (!isWordAdded)
			{
				ErrorAddingMessage = $"The word - {OneWord.Definition.Trim()} - is already in database.";
			}


			// use cookies to add error message if the word already exists
			HttpContext.Response.Cookies.Append("error", ErrorAddingMessage);

			HttpContext.Response.Redirect("WordPage");
		}

		// asp-page-handler is used in form with 'delete' value
		public async Task OnPostDeleteAsync(string word, string meaning)
		{
			if (_username is not null)
			{
				await new DeleteWord(_ctx).DoAsync(word, meaning, _username);
			}

			HttpContext.Response.Redirect("WordPage");
			//return RedirectToPage("WordPage");
		}

		public async Task OnPostNewToOldAsync ()
		{
			OrderWords = WordsOrder.NewestToOldest;
			OnGet();
		}

		public async Task OnPostOldToNewAsync()
		{
			OrderWords = WordsOrder.OldestToNewest;
			OnGet();
		}
	}
}
