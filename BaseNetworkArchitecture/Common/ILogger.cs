using System;

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