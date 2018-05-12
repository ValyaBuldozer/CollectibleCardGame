using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BaseNetworkArchitecture.Common;

namespace CollectibleCardGame
{
    public class MessageBoxLogger : ILogger
    {
        private readonly Dispatcher _uiDispatcher;

        public MessageBoxLogger()
        {
            _uiDispatcher = Dispatcher.CurrentDispatcher;
        }

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
            try
            {
                _uiDispatcher.Invoke(() =>
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show(message);
                });

            }
            catch (InvalidOperationException e)
            {
                
            }
        }

        public void Log(Exception e)
        {
            //log exception
        }
    }
}
