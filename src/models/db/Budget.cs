using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("budgets")]
	[PrimaryKey(nameof(Username), nameof(ValidUntil))]
	public class Budget
	{
		[Column("id")]
		public int ID { get; set; }

		[Column("username")]
		public string Username { get; set; }

		[Column("valid_until")]
		public long ValidUntil { get; set; }

		[Column("amount")]
		public Double Amount { get; set; }
	}
}