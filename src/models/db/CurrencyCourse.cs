using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("currency_courses")]
	[PrimaryKey(nameof(BaseID), nameof(ComparedID))]
	public class CurrencyCourse
	{
		[Column("base_id")]
		public Double BaseID { get; set; }

		[Column("compared_id")]
		public Double ComparedID { get; set; }

		[Column("course")]
		public Double Course { get; set; }
	}
}
