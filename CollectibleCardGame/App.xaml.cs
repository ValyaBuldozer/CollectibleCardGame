using System.Net.Sockets;
using System.Windows;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            UnityKernel.InitializeKernel();
            UnityKernel.Get<MainWindow>().Show();
            UnityKernel.Get<IGlobalController>().OnStartup();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            var networkControlller = UnityKernel.Get<INetworkController>();

            try
            {
                if (networkControlller.ServerCommunicator.IsConnected)
                    networkControlller.Disconnect();
            }
            catch (SocketException)
            {
            }
        }
    }
}