using System;
using GameData.Enums;
using GameData.Models.PlayerTurn;

namespace CollectibleCardGame.Services
{
    public class LogInRegisterRequestEventArgs : EventArgs
    {
        public LogInRegisterRequestEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }

    public class GameRequestEventArgs : EventArgs
    {
        public GameRequestEventArgs(Fraction fraction)
        {
            Fraction = fraction;
        }

        public Fraction Fraction { get; }
    }

    public class PlayerTurnRequestEventArgs : EventArgs
    {
        public PlayerTurnRequestEventArgs(PlayerTurn playerTurn)
        {
            PlayerTurn = playerTurn;
        }

        public PlayerTurn PlayerTurn { get; }
    }
}