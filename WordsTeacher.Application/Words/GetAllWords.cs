using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.Application.Words
{
	public class GetAllWords
	{
		private ApplicationContext _ctx;

		public GetAllWords(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public IEnumerable<Word> Do()
		{
			return _ctx.Words.ToList();
		}
	}
}
