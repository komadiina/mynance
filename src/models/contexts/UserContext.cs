using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Pomelo.EntityFrameworkCore.MySql;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace mynance.src.models.contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfig =
                new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build().GetSection("db");

            String? host = dbConfig["host"],
                    user = dbConfig["user"],
                    password = dbConfig["password"],
                    database = dbConfig["database"],
                    port = dbConfig["port"];

            if (host == null || user == null || password == null || database == null || port == null)
                throw new Exception("Database configuration not initialized properly. Did you check appsettings.json?");

            String connectionString = String.Format("server={0};port={1};user={2};password={3};database={4}", host, port, user, password, database);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
