using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;

namespace WordsTeacher.Application.Words
{
	public class DeleteWord
	{
		private readonly ApplicationContext _context;
		
		public DeleteWord(ApplicationContext context)
		{
			_context = context;
		}


		public void Do(string word, string nick)
		{
			// to do deletion word. maybe with other parameters
		}
	}
}
