using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Network.Controllers;

namespace CollectibleCardGame.Logic.Controllers
{
    public interface IGlobalController
    {
        void OnStartup(string adress,int port);
        void OnConnectionLost();
        void OnClose();
        bool TryConnect(string adress,int port);
    }
}
