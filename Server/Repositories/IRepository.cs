using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Collection { set; get; }
        void Update();
    }
}
