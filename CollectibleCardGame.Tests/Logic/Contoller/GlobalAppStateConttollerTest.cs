using System.Net;
using BaseNetworkArchitecture.Server;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Unity;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.Views.Frames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.Logic.Contoller
{
    [TestClass]
    public class GlobalAppStateConttollerTest
    {
        // [TestMethod]
        public void OnStartupTest()
        {
            IServer server = new TcpServer();
            UnityKernel.InitializeKernel();
            var globalAppStateController = UnityKernel.Get<GlobalAppStateController>();
            var connectionController = UnityKernel.Get<INetworkController>();

            server.Start(IPAddress.Parse("127.0.0.1"), 8800);
            globalAppStateController.OnStartup();

            Assert.IsTrue(connectionController.ServerCommunicator.IsConnected);
        }

        // [TestMethod]
        public void OnStartupNoConnectionTest()
        {
            UnityKernel.InitializeKernel();
            var globalAppStateController = UnityKernel.Get<GlobalAppStateController>();
            var framePage = UnityKernel.Get<LogInFramePageShellViewModel>();
            var connectionController = UnityKernel.Get<INetworkController>();

            // globalAppStateController.OnStartup("127.0.0.1",8800);

            Assert.IsFalse(connectionController.ServerCommunicator.IsConnected);
            Assert.IsTrue(framePage.CurrentFramePage is ConnectionErrorFramePage);
        }
    }
}