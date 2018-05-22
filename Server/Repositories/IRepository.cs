using System.Collections.Generic;

namespace Server.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Collection { set; get; }
        void Update();
    }
}