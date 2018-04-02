using System.Collections.Generic;

namespace BaseNetworkArchitecture.Server
{
    public interface IServer
    {
        ICollection<IClientConnection> Clients { set; get; }
        void Start();
        void Stop();
    }
}