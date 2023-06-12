using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Domain
{
	public class User : IUser
	{
		public string NickName { get; set; } = "";
	}
}
