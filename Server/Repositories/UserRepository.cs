using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using Server.Database;
using Server.Models;

namespace Server.Repositories
{
    public class UserRepository
    {
        private readonly IContext _context;

        public DbSet<User> DatabaseCollection { get; }

        public List<User> Collection { get; }

        public bool IsDatabaseConnected { get; }

        public UserRepository(IContext context,ILogger logger)
        {
            _context = context;
            IsDatabaseConnected = _context.IsDatabaseExists();

            if(IsDatabaseConnected)
                DatabaseCollection = context.Users;
            else
            {
                logger.LogAndPrint(
                    "Error : Database was not found. The server continue " +
                    "its work without saving the user state.");
                Collection = new List<User>();
            }
        }

        public void Update()
        {
            if (_context.IsDatabaseExists())
                _context.Save();
        }
    }
}
