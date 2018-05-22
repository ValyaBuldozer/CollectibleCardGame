using System;

namespace Server.Exceptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(string message) : base(message)
        {
        }
    }
}