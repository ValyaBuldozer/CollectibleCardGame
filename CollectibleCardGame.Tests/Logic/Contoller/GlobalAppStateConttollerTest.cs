using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Server;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using CollectibleCardGame.Views.Frames;
using GameData.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectibleCardGame.Unity;

namespace CollectibleCardGame.Tests.Logic.Contoller
{
    [TestClass]
    public class GlobalAppStateConttollerTest
    {
        [TestMethod]
        public void OnStartupTest()
        {
            IServer server = new TcpServer();
            UnityKernel.InitializeKernel();
            var globalAppStateController = UnityKernel.Get<GlobalAppStateController>();

            server.Start(IPAddress.Parse("127.0.0.1"), 8800);
            globalAppStateController.OnStartup();

            Assert.IsTrue(globalAppStateController.ConnectionController.ServerCommunicator.IsConnected);
        }

        [TestMethod]
        public void OnStartupNoConnectionTest()
        {
            UnityKernel.InitializeKernel();
            var globalAppStateController = UnityKernel.Get<GlobalAppStateController>();
            var framePage = UnityKernel.Get<LogInFramePageShellViewModel>();

            globalAppStateController.OnStartup();

            Assert.IsFalse(globalAppStateController.ConnectionController.ServerCommunicator.IsConnected);
            Assert.IsTrue(framePage.CurrentFramePage is ConnectionErrorFramePage);
        }
    }
}
