using Microsoft.EntityFrameworkCore;

namespace mynance.src.models.db.contexts
{
	class UserPreferenceContext : MynanceContext
	{
		public DbSet<UserPreference> UserPreferences { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserPreference>()
				.Property(x => x.Username).HasColumnName("username");
		}
	}
}
