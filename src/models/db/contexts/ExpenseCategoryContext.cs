using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	class ExpenseCategoryContext : DbContext
	{
		public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ExpenseCategory>()
				.Property(x => x.Id).HasColumnName("id");
		}
	}
}
