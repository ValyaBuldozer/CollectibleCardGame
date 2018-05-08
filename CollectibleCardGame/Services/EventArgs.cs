using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectibleCardGame.Services
{
    public class LogInRegisterRequestEventArgs : EventArgs
    {
        public string Username { get; }

        public string Password { get; }

        public LogInRegisterRequestEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class GameRequestEventArgs : EventArgs
    {
        public string Fraction { get; }

        public GameRequestEventArgs(string fraction)
        {
            Fraction = fraction;
        }
    }
}
