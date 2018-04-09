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
    public class UserRepository
    {
        private readonly IContext _context;

        public DbSet<User> Collection { set; get; }

        public UserRepository(IContext context)
        {
            _context = context;
            Collection = context.Users;
        }

        public void Update()
        {
            _context.Save();
        }
    }
}
