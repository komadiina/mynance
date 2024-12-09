using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	class AssignedBudgetContext : MynanceContext
	{
		public DbSet<AssignedBudget> AssignedBudgets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AssignedBudget>()
				.Property(x => x.ID)
				.HasColumnName("id")
				.ValueGeneratedOnAdd();
		}
	}
}
