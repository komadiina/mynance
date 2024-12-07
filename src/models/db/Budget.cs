using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("budgets")]
	[PrimaryKey(nameof(Username), nameof(ValidUntil), nameof(CategoryID))]
	public class Budget
	{
		[Column("username")]
		public string Username { get; set; }

		[Column("valid_until")]
		public long ValidUntil { get; set; }

		[Column("category_id")]
		public int CategoryID { get; set; } // ...foreign key constraint applied

		[Column("amount")]
		public Double Amount { get; set; }
	}
}