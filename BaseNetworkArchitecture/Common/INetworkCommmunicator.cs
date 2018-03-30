namespace BaseNetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool SendMessage(NetworkMessage networkMessage);
        NetworkMessage ReadMessage();
        bool Connect();
        bool Disconnect();
    }
}