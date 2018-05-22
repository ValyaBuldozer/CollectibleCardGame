using System;

namespace Server
{
    public class GameLobbyCloseEventArgs : EventArgs
    {
        public GameLobbyCloseEventArgs(string winnerUsername)
        {
            WinnerUsername = winnerUsername;
        }

        public string WinnerUsername { get; }
    }
}