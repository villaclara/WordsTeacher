using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;
using WordsTeacher.Domain;

namespace WordsTeacher.Application.Words
{
    public class CreateWord
    {
        private ApplicationContext _context;

        public CreateWord(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Do (Word wordmodel)
        {
            //var alreadyExistsWord = _context.Words.First(delegate (Word word)
            //{
            //    return word.Definition == wordmodel.Definition;
            //});

            var alreadyExistsWord = _context.Words.FirstOrDefault(w =>  w.Definition == wordmodel.Definition && w.NickName == wordmodel.NickName);

            if (alreadyExistsWord == null)
            {
                _context.Words.Add(wordmodel);
				await _context.SaveChangesAsync();
				return true;
            }

            return false;
        }
    }
}
