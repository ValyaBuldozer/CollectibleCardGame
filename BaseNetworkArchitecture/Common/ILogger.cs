using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public interface ILogger
    {
        void Print(string info);
        void Log(string info);
        void LogAndPrint(string info);
        void Log(Exception e);
    }
}
