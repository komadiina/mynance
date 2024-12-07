using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[PrimaryKey(nameof(Username))]
	public class User
	{
		[Column("username")]
		public string Username { get; set; }

		[Column("fullname")]
		public string FullName { get; set; }

		[Column("password")]
		public string Password { get; set; }

		[Column("registration_date")]
		public DateTime RegistrationDate { get; set; }

		[Column("last_active")]
		public DateTime LastActive { get; set; }

		[Column("active")]
		public Boolean IsActive { get; set; }

		public override string ToString()
		{
			return String.Format("{0} {1} {2} {3} {4}", Username, FullName, RegistrationDate.ToString(), LastActive.ToString(), IsActive.ToString());
		}
	}
}
