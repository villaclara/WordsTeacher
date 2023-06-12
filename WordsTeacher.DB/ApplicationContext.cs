using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Domain;

namespace WordsTeacher.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Word> Words { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

    }
}
