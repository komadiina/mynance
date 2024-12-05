using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	public class CurrencyCourseContext : MynanceContext
	{
		public DbSet<CurrencyCourse> CurrencyCourses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CurrencyCourse>()
				.Property(x => x.BaseID).HasColumnName("base_id");

			modelBuilder.Entity<CurrencyCourse>()
				.Property(x => x.ComparedID).HasColumnName("compared_id");
		}
	}
}
