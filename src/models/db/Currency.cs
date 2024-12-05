using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
	[Table("currencies")]
	[PrimaryKey(nameof(NumericCode))]
	public class Currency
	{
		[Column("numeric_code")]
		public int NumericCode { get; set; }

		[Column("alphabetic_code")]
		public String AlphabeticCode { get; set; }

		[Column("currency")]
		public String DisplayName { get; set; }
	}
}
