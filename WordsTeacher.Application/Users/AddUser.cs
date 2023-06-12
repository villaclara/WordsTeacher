using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;

namespace WordsTeacher.Application.Users
{
	public class AddUser
	{
		private ApplicationContext _ctx;

		public AddUser(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public void Do(string username)
		{
			
		}
	}
}
