using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;

namespace WordsTeacher.Application.Users
{
	public class GetUsers
	{
		private ApplicationContext _ctx;

		public GetUsers(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public IEnumerable<IdentityUser> Do()
		{
			return _ctx.Users;
		}
	}
}
