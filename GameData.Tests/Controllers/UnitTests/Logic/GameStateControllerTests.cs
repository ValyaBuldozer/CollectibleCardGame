using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameData.Tests.Controllers.UnitTests.Logic
{
    [TestClass]
    public class GameStateControllerTests
    {
        [TestMethod]
        public void StartTest()
        {
            TestCards testCards = new TestCards();
            TableCondition tableCondition = new TableCondition();

            var firstPLayerDeck = new Stack<ICard>();
            firstPLayerDeck.Push(new UnitCard()
            {
                Name = "testCard1",
                Description = "Test"
            });
            firstPLayerDeck.Push(new SpellCard()
            {
                Name = "spellCard1",
                Description = "Test"
            });
            var secondPlayerDeck = new System.Collections.Generic.Stack<ICard>(firstPLayerDeck);

            var deckControllerMock = new Mock<IDeckController>();
            deckControllerMock.Setup(mock => mock.AddDeck("ping", null));
            deckControllerMock.Setup(mock => mock.PopCards("firstPlayer", 4)).Returns(new List<ICard>());
            deckControllerMock.Setup(mock => mock.PopCards("secondPlayer", 4)).Returns(new List<ICard>());

            var playerTurnDispatcherMock = new Mock<IPlayerTurnDispatcher>();
            playerTurnDispatcherMock.Setup(mock => mock.Start(It.IsAny<double>()));

            var gameStateController =
                new GameStateController(tableCondition,playerTurnDispatcherMock.Object)
                {
                    DeckController = deckControllerMock.Object
                };

            gameStateController.Start(firstPLayerDeck,"firstPlayer",testCards.FirstCard
                ,secondPlayerDeck,"secondPlayer",testCards.SecondCard);

            var firstPlayer = tableCondition.Players.FirstOrDefault(
                p => p.Username == "firstPlayer");
            var secondPlayer = tableCondition.Players.FirstOrDefault(
                p => p.Username == "secondPlayer");

            playerTurnDispatcherMock.Verify(mock=>mock.Start(It.IsAny<double>()),Times.Once);
            deckControllerMock.Verify(foo=>foo.AddDeck(It.IsAny<string>(),It.IsAny<Stack<ICard>>()),Times.AtLeastOnce);
            deckControllerMock.Verify(mock=>mock.PopCards("firstPlayer", 4),Times.Once);
            deckControllerMock.Verify(mock=>mock.PopCards("secondPlayer", 4),Times.Once);

            Assert.AreEqual(2,tableCondition.Players.Count);
            Assert.IsNotNull(firstPlayer,"Первый игрок не найден");
            Assert.IsNotNull(secondPlayer, "Второй игрок не найден");
        }
    }
}
