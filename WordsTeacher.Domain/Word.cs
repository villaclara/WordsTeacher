﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Domain
{
    public class Word
    {

        public string Definition { get; set; } = "";
        public string Meaning { get; set; } = "";

        public long Id { get; set; }

        public string NickName { get; set; } = "";

        
    }
}
