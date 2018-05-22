using System;

namespace GameData.Exceptions
{
    public class PlayerTurnValidationException : Exception
    {
        public PlayerTurnValidationException(string message) : base(message)
        {
        }
    }
}