using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	public class CurrencyContext : MynanceContext
	{
		public DbSet<Currency> Currencies { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Currency>()
				.Property(x => x.NumericCode).HasColumnName("numeric_code");
		}
	}
}
