using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("payments")]
	[PrimaryKey(nameof(ID))]
	class Payment
	{
		[Column("id")]
		public int ID { get; set; }

		[Column("username")]
		public String Username { get; set; }

		[Column("budget_id")]
		public int BudgetID { get; set; }

		[Column("assigned_budget_id")]
		public int AssignedBudgetID { get; set; }

		[Column("amount")]
		public Double Amount { get; set; }

		[Column("date_time")]
		public long DateTime { get; set; }

		[Column("outgoing")]
		public Boolean Outgoing { get; set; }

		[Column("note")]
		public String Note { get; set; }
	}
}
