using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame
{
    public class MessageBoxLogger : ILogger
    {
        public void Print(string message)
        {
            MessageBox.Show(message);
        }

        public void Log(string message)
        {
            //log message
        }

        public void LogAndPrint(string message)
        {
            MessageBox.Show(message);
        }

        public void Log(Exception e)
        {
            //log exception
        }
    }
}
