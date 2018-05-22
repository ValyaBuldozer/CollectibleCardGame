using System.Data.Entity;
using Server.Models;

namespace Server.Database
{
    public class AppDbContext : DbContext, IContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<UserInfo> UsersInfo { set; get; }

        public void Save()
        {
            SaveChanges();
        }

        public bool IsDatabaseExists()
        {
            return Database.Exists();
        }
    }
}