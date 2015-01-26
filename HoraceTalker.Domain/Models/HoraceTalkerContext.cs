using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using HoraceTalker.Domain.Models.Mapping;

namespace HoraceTalker.Domain.Models
{
    public partial class HoraceTalkerContext : DbContext
    {
        static HoraceTalkerContext()
        {
            Database.SetInitializer<HoraceTalkerContext>(null);
        }

        public HoraceTalkerContext()
            : base("Name=HoraceTalkerContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
