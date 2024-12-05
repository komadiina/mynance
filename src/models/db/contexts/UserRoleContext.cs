using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	class UserRoleContext : MynanceContext
	{
		public DbSet<UserRole> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserRole>()
				.Property(x => x.Username).HasColumnName("username");

			modelBuilder.Entity<UserRole>()
				.Property(x => x.Role).HasColumnName("role");
		}
	}
}
