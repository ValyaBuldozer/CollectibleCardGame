using System;
using System.Windows;
using System.Windows.Threading;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Unity;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

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
                    MessageBox.Show(UnityKernel.Get<MainWindow>(), message, "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
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