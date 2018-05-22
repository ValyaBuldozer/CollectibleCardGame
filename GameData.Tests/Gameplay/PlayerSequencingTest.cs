using System.Linq;
using System.Threading;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Gameplay
{
    [TestClass]
    public class PlayerSequencingTest
    {
        [TestMethod]
        public void TimerTest()
        {
            var testCards = new TestCards();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);
            var observerRepository = container.Get<ObserverActionRepository>();
            var turnDispatcher = container.Get<IPlayerTurnDispatcher>();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            var currentPlayer = turnDispatcher.CurrentPlayer;

            var firstTurnObserver = observerRepository.Collection.FirstOrDefault(
                o => o.Type == ObserverActionType.TurnStart);

            Assert.IsNotNull(firstTurnObserver);

            observerRepository.Collection.Remove(firstTurnObserver);

            Thread.Sleep(40000);

            var secondTurnObserver = observerRepository.Collection.FirstOrDefault(
                o => o.Type == ObserverActionType.TurnStart);

            Assert.IsNotNull(secondTurnObserver);
            Assert.AreNotEqual(currentPlayer, turnDispatcher.CurrentPlayer);
        }

        [TestMethod]
        public void PlayerTurnSkipTest()
        {
            var testCards = new TestCards();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);
            var observerRepository = container.Get<ObserverActionRepository>();
            var turnDispatcher = container.Get<IPlayerTurnDispatcher>();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            var currentPlayer = turnDispatcher.CurrentPlayer;

            var firstTurnObserver = observerRepository.Collection.FirstOrDefault(
                o => o.Type == ObserverActionType.TurnStart);

            Assert.IsNotNull(firstTurnObserver);

            var turnSkipPlayerTurn = new EndPlayerTurn(currentPlayer);
            container.Get<IPlayerTurnHandler<EndPlayerTurn>>().Execute(turnSkipPlayerTurn);

            var secondTurnObserver = observerRepository.Collection.FirstOrDefault(
                o => o.Type == ObserverActionType.TurnStart);

            Assert.IsNotNull(secondTurnObserver);
            Assert.AreNotEqual(currentPlayer, turnDispatcher.CurrentPlayer);
        }
    }
}