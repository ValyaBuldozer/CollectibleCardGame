using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Exceptions
{
    public class PlayerTurnValidationException : Exception
    {
        public PlayerTurnValidationException(string message) : base(message) { }
    }
}
