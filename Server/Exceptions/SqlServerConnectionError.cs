using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class SqlServerConnectionError : Exception
    {
        public SqlServerConnectionError(string message) : base(message)
        {

        }
    }
}
