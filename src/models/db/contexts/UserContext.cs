using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	public class UserContext : MynanceContext
	{
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.Property(x => x.Username).HasColumnName("username");
		}
	}
}
