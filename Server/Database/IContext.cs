using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Database
{
    public interface IContext
    {
        DbSet<User> Users { get; }
        void Save();
    }
}
