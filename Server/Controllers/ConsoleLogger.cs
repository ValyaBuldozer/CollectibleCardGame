using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;

namespace Server.Controllers
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {

        }

        public void Print(string info)
        {
            if(!string.IsNullOrEmpty(info))
                Console.WriteLine(info);
        }

        public void Log(string log)
        {
            //todo : логгирование
        }

        public void LogAndPrint(string info)
        {
            if (!string.IsNullOrEmpty(info))
                Console.WriteLine(info);
        }

        public void Log(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
