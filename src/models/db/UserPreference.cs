using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[PrimaryKey(nameof(Username))]
	[Table("preferences")]
	class UserPreference
	{
		[Column("username")]
		public string Username { get; set; }

		[Column("use_dark_mode")]
		public bool UseDarkTheme { get; set; }

		[Column("locale")]
		public string LocaleName { get; set; }

		[Column("currency_id")]
		public int CurrencyID { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			else if (obj is not UserPreference) return false;
			UserPreference pref = (UserPreference)obj;

			return pref.Username == Username && pref.UseDarkTheme == UseDarkTheme && pref.LocaleName == LocaleName;
		}
	}
}
