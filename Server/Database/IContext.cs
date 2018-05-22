using System.Data.Entity;
using Server.Models;

namespace Server.Database
{
    public interface IContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserInfo> UsersInfo { get; set; }
        void Save();
        bool IsDatabaseExists();
    }
}