using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Domain
{
	public class User : IUser
	{
		[DataType(DataType.EmailAddress)]
		public string NickName { get; set; } = "";
	}
}
