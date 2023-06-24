using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.DB;

namespace WordsTeacher.Application.Users
{
	public class DeleteUser
	{
		private ApplicationContext _ctx;

		public DeleteUser(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public async Task Do(string user)
		{
			var us = _ctx.Users.Where(u => u.UserName == user).First();
			_ctx.Users.Remove(us);
			await _ctx.SaveChangesAsync();
		}
	}
}
