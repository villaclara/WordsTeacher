using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;

namespace WordsTeacher.Application.Words
{
	public class GetWords
	{
		private ApplicationContext _ctx;

		public GetWords(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

		public IEnumerable<WordViewModel> Do(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return Enumerable.Empty<WordViewModel>();
			}

			return (IEnumerable<WordViewModel>)_ctx.Words.ToList().Select(x => x.NickName == username);



		}
    }
}
