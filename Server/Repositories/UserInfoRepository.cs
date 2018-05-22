using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Server.Models;

namespace Server.Repositories
{
    public class UserInfoRepository
    {
        private readonly IContext _context;

        public DbSet<UserInfo> Collection { set; get; }

        public UserInfoRepository(IContext context)
        {
            _context = context;

            if(_context.IsDatabaseExists())
                Collection = context.UsersInfo;
        }

        public void Update()
        {
            _context.Save();
        }
    }
}
