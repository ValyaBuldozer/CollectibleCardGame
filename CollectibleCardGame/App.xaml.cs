using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CollectibleCardGame;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Unity;
using CollectibleCardGame.ViewModels;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.ViewModels.Windows;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using CollectibleCardGame.Views.UserControls;
using GameData.Models.Cards;
using GameData.Network.Messages;
using Unity;

namespace CollectibleCardGame
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
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
            catch(SocketException) { }

            
        }
    }
}
