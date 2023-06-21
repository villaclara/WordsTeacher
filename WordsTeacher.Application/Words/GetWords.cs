using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.Application.Words
{
	public class GetWords
	{
		private ApplicationContext _ctx;

		public GetWords(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

		public IEnumerable<Word> Do(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return Enumerable.Empty<Word>();
			}
			return _ctx.Words.ToList().Where(x => x.NickName == username) ?? Enumerable.Empty<Word>();
		}
    }
}
