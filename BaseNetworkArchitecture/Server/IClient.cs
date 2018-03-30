using BaseNetworkArchitecture.Common;

namespace BaseNetworkArchitecture.Server
{
    public interface IClient
    {
        INetworkCommunicator Communicator { set; get; }
        string IdentificatorTocken { set; get; }
        bool IsInSystem { set; get; }
    }
}