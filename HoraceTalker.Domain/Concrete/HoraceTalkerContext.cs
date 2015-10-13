using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;
using HoraceTalker.Domain.Models;

namespace HoraceTalker.Domain.Concrete
{
    public partial class HoraceTalkerContext : DbContext
    {
        public HoraceTalkerContext()
        {
            CreateDatabase();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "horacetalkerdb.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public DbSet<User> Users { get; set; }

        private void CreateDatabase()
        {
            Database.EnsureCreated();
        }
    }
}
