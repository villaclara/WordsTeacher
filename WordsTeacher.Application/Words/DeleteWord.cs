using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.Application.Words
{
	public class DeleteWord
	{
		private readonly ApplicationContext _context;
		
		public DeleteWord(ApplicationContext context)
		{
			_context = context;
		}

		public async Task DoAsync(string word, string meaning, string nick)
		{
			try
			{
				_context.Remove(_context.Words.Single(w => w.Definition == word && w.Meaning == meaning && w.NickName == nick));
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync(ex.Message);
            }
				
		}
	}
}
