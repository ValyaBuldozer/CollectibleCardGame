using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GameLobbyCloseEventArgs : EventArgs
    {
        public string WinnerUsername { get; }

        public GameLobbyCloseEventArgs(string winnerUsername)
        {
            WinnerUsername = winnerUsername;
        }
    }
}
