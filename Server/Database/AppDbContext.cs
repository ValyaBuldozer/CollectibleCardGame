using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Server.Models;

namespace Server.Database
{
    public class AppDbContext : DbContext, IContext
    {
        public AppDbContext()
        {
            //System.Data.Entity.Database.SetInitializer(new AppDbContextInitializer());
        }

        public DbSet<User> Users { set; get; }
        public DbSet<UserInfo> UsersInfo { set; get; }

        public void Save()
        {
            SaveChanges();
        }
    }
}
