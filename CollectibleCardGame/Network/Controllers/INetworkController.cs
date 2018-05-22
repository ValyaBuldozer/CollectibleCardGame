using System.Net;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers
{
    public interface INetworkController
    {
        INetworkCommunicator ServerCommunicator { set; get; }
        void Connect(IPAddress ipAddress, int port);
        void Disconnect();
        void SendMessage(MessageBase message);
        void OnMessageRecieved(object sender, MessageEventArgs e);
        void OnBreakConnection(object sender, BreakConnectionEventArgs e);
    }
}