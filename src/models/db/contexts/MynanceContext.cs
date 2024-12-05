using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace mynance.src.models.db.contexts
{
	public class MynanceContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var dbConfig =
				new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build().GetSection("db");

			string? host = dbConfig["host"],
					user = dbConfig["user"],
					password = dbConfig["password"],
					database = dbConfig["database"],
					port = dbConfig["port"];

			if (host == null || user == null || password == null || database == null || port == null)
				throw new Exception("Database configuration not initialized properly. Did you check appsettings.json?");

			string connectionString = string.Format("server={0};port={1};user={2};password={3};database={4}", host, port, user, password, database);
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
