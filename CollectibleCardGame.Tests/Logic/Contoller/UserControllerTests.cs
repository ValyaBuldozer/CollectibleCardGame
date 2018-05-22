using System.Net;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using GameData.Network.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectibleCardGame.Tests.Logic.Contoller
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void LogInRequestTest()
        {
            var testController = new TestNetworkController();
            //var mock = new Mock<INetworkController>();

            var controller = new UserController(null, testController,
                new LogInFramePageViewModel(), new RegistrationFramePageViewModel());

            controller.LogInRequest("test", "test");

            Assert.IsTrue(testController.LastMethodName == "SendMessage" &&
                          testController.LastMessage.Content is LogInMessage);
        }

        [TestMethod]
        public void RegistrationRequestTest()
        {
            var testController = new TestNetworkController();
            var controller = new UserController(null, testController,
                new LogInFramePageViewModel(), new RegistrationFramePageViewModel());

            controller.RegistrationRequest("test", "test");

            Assert.IsTrue(testController.LastMethodName == "SendMessage" &&
                          testController.LastMessage.Content is RegistrationMessage);
        }

        [TestMethod]
        public void SetUserTest()
        {
            var testController = new TestNetworkController();
            var controller = new UserController(null, testController,
                new LogInFramePageViewModel(), new RegistrationFramePageViewModel());

            controller.SetUser("test");

            Assert.IsTrue(controller?.CurrentUserService.Username == "test");
        }

        [TestMethod]
        public void ResetUser()
        {
            var testController = new TestNetworkController();
            var controller = new UserController(null, testController,
                new LogInFramePageViewModel(), new RegistrationFramePageViewModel());

            controller.ResetUser();

            Assert.IsTrue(controller?.CurrentUserService == null);
        }
    }

    public class TestNetworkController : INetworkController
    {
        public string LastMethodName { private set; get; }

        public MessageBase LastMessage { private set; get; }

        public INetworkCommunicator ServerCommunicator { set; get; }

        public void Connect(IPAddress ipAddress, int port)
        {
            LastMethodName = nameof(Connect);
        }

        public void Disconnect()
        {
            LastMethodName = nameof(Disconnect);
        }

        public void SendMessage(MessageBase message)
        {
            LastMethodName = nameof(SendMessage);
            LastMessage = message;
        }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            LastMethodName = nameof(OnMessageRecieved);
        }

        public void OnBreakConnection(object sender, BreakConnectionEventArgs e)
        {
            LastMethodName = nameof(OnBreakConnection);
        }
    }
}