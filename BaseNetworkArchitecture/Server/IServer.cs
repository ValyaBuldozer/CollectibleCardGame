using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Server
{
    public interface IServer
    {
        ICollection<IClient> Clients { set; get; }
        void Start();
        void Stop();
    }
}