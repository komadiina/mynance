using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[PrimaryKey(nameof(Id))]
	[Table("expense_categories")]
	class ExpenseCategory
	{
		[Column("id")]
		public int Id { get; private set; }

		[Column("title")]
		public string Title { get; set; }
	}
}
