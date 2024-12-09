using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	class PaymentsContext : MynanceContext
	{
		public DbSet<Payment> Payments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Payment>()
				.Property(x => x.ID)
				.HasColumnName("id")
				.ValueGeneratedOnAdd();
		}
	}
}
