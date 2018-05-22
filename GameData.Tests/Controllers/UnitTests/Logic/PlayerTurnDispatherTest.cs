using System.Threading;
using GameData.Controllers.Global;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameData.Tests.Controllers.UnitTests.Logic
{
    [TestClass]
    public class PlayerTurnDispatherTest
    {
        [TestMethod]
        public void StartTest()
        {
            var tableCondition = new TestTableCondition().GetFirstCondition;
            var dealCardsDispather = new Mock<ICardDrawController>();
            dealCardsDispather.Setup(mock => mock.DealCardsToPlayer(It.IsAny<Player>(), It.IsAny<int>()));
            var settings = new GameSettings
            {
                IsPlayerTurnTimerEnabled = true,
                PlayerTurnInterval = 5
            };
            var playerTurnDispatcher = new PlayerTurnDispatcher
                (tableCondition, dealCardsDispather.Object, settings);
            var eventWasDispatchered = false;
            playerTurnDispatcher.TurnStart += (sender, args) => eventWasDispatchered = true;

            playerTurnDispatcher.Start();
            Thread.Sleep(30);

            Assert.IsTrue(eventWasDispatchered);
        }

        [TestMethod]
        public void NextPlayerTest()
        {
            var tableCondition = new TestTableCondition().GetFirstCondition;
            var dealCardsDispather = new Mock<ICardDrawController>();
            dealCardsDispather.Setup(mock => mock.DealCardsToPlayer(It.IsAny<Player>(), It.IsAny<int>()));
            var playerTurnDispatcher = new PlayerTurnDispatcher(
                tableCondition, dealCardsDispather.Object, TestGameSettings.Get);

            var currentPlayer = playerTurnDispatcher.CurrentPlayer;
            var eventWasDispatchered = false;
            playerTurnDispatcher.TurnStart += (sender, args) => eventWasDispatchered = true;

            playerTurnDispatcher.NextPlayer();

            dealCardsDispather.Verify(mock => mock.DealCardsToPlayer(It.IsAny<Player>(), 1), Times.AtLeastOnce);

            Assert.IsTrue(eventWasDispatchered);
            Assert.AreNotEqual(currentPlayer, playerTurnDispatcher.CurrentPlayer);
        }
    }
}