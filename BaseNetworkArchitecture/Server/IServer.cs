using System.Collections.Generic;

namespace BaseNetworkArchitecture.Server
{
    public interface IServer
    {
        ICollection<IClient> Clients { set; get; }
        void Start();
        void Stop();
    }
}