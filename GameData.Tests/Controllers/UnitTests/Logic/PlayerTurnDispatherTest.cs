using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameData.Controllers.Data;
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
            PlayerTurnDispatcher playerTurnDispatcher = new PlayerTurnDispatcher
                (tableCondition,dealCardsDispather.Object);
            bool eventWasDispatchered = false;
            playerTurnDispatcher.TurnStart += (sender, args) => eventWasDispatchered = true;

            playerTurnDispatcher.Start(5);
            Thread.Sleep(30);

            Assert.IsTrue(eventWasDispatchered);
        }

        [TestMethod]
        public void NextPlayerTest()
        {
            var tableCondition = new TestTableCondition().GetFirstCondition;
            var dealCardsDispather = new Mock<ICardDrawController>();
            dealCardsDispather.Setup(mock => mock.DealCardsToPlayer(It.IsAny<Player>(), It.IsAny<int>()));
            PlayerTurnDispatcher playerTurnDispatcher = new PlayerTurnDispatcher(
                tableCondition,dealCardsDispather.Object);

            var currentPlayer = playerTurnDispatcher.CurrentPlayer;
            bool eventWasDispatchered = false;
            playerTurnDispatcher.TurnStart += (sender, args) => eventWasDispatchered = true;

            playerTurnDispatcher.NextPlayer();

            dealCardsDispather.Verify(mock=>mock.DealCardsToPlayer(It.IsAny<Player>(),1),Times.AtLeastOnce);

            Assert.IsTrue(eventWasDispatchered);
            Assert.AreNotEqual(currentPlayer,playerTurnDispatcher.CurrentPlayer);
        }
    }
}
