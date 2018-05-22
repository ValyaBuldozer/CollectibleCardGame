using System.Data.Entity;
using Server.Database;
using Server.Models;

namespace Server.Repositories
{
    public class UserInfoRepository
    {
        private readonly IContext _context;

        public UserInfoRepository(IContext context)
        {
            _context = context;

            if (_context.IsDatabaseExists())
                Collection = context.UsersInfo;
        }

        public DbSet<UserInfo> Collection { set; get; }

        public void Update()
        {
            _context.Save();
        }
    }
}