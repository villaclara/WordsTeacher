using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Application
{
    // is used for database when adding new words
    // 
    public class WordViewModel
    {
        public string Definition { get; set; } = "";
        public string Meaning { get; set; } = "";
        public string NickName { get; set; } = "";

        public long Id { get; set; }

    }
}
