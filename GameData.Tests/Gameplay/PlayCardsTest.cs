using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models;
using GameData.Models.Observer;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Gameplay
{
    [TestClass]
    public class PlayCardsTest
    {
        [TestMethod]
        public void PlayUnitCardTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            


            //var observerRepository = container.Get<ObserverActionRepository>();
            //var startGameObserver =
            //    observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameStart);

            var player = container.Get<TableCondition>().Players.First();
            var card = player.HandCards.First();
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);

            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(6,((CardDeployObserverAction)startGameObserver).Card.ID);
           // Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
           // Assert.AreEqual(5, container.Get<TableCondition>().Players.First().TableUnits[0].HealthPoint);

        }
    }
}
