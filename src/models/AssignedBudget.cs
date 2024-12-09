using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("assigned_budgets")]
	[PrimaryKey(nameof(ID))]
	public class AssignedBudget
	{
		[Column("id")]
		public int ID { get; set; }

		[Column("budget_id")]
		public int BudgetID { get; set; }

		[Column("category_id")]
		public int CategoryID { get; set; }

		[Column("amount")]
		public Double Amount { get; set; }
	}
}