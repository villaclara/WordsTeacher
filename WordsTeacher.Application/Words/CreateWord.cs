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

        public async Task Do (WordViewModel wordmodel)
        {
            _context.Words.Add(new Word
            {
                NickName = wordmodel.NickName,
                Definition = wordmodel.Definition,
                Meaning = wordmodel.Meaning
            });

            await _context.SaveChangesAsync();
        }
    }
}
