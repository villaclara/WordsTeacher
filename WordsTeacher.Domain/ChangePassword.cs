using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Domain
{
	public class ChangePassword
	{
		public int Username { get; set; }
		public string OldPwd { get; set; } = "";
		public string NewPwd { get; set; } = "";
		public string ConfirmedNewPwd { get; set; } = "";
	}
}
