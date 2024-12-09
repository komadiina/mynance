using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	public class BudgetContext : MynanceContext
	{
		public DbSet<Budget> Budgets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Budget>()
				.Property(x => x.Username).HasColumnName("username");

			modelBuilder.Entity<Budget>()
				.Property(b => b.ID)
				.ValueGeneratedOnAdd();
		}
	}
}
