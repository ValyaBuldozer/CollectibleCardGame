using System.Net;

namespace CollectibleCardGame.Logic.Controllers
{
    public interface IGlobalController
    {
        void OnStartup();
        void OnConnectionLost();
        void OnClose();
        bool TryConnect(IPAddress adress, int port);
    }
}