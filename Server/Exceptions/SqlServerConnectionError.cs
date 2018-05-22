using System;

namespace Server.Exceptions
{
    public class SqlServerConnectionError : Exception
    {
        public SqlServerConnectionError(string message) : base(message)
        {
        }
    }
}