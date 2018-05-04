using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Global;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.Repository;
using GameData.Models.Units;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Gameplay
{
    [TestClass]
    public class GameStartTest
    {
        /// <summary>
        /// Проверка старта игры - только наличие колод и игроков
        /// </summary>
        [TestMethod]
        public void StartGame()
        {
            var testCards = new TestCards();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck,"FirstPlayer", testCards.FirstCard,
                secondDeck,"SecondPlayer",testCards.SecondCard);

            Assert.AreEqual(container.Get<TableCondition>().Players.Count,2);
            Assert.AreEqual(container.Get<IDeckController>().GetDeck("FirstPlayer"),firstDeck);
            Assert.AreEqual(container.Get<IDeckController>().GetDeck("SecondPlayer"),secondDeck);
        }

        [TestMethod]
        public void StartGameObserverTest()
        {
            var testCards = new TestCards();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameStart);
            

            Assert.AreNotEqual(startGameObserver,null);
            Assert.IsTrue(startGameObserver is GameStartObserverAction action);
            Assert.AreEqual(((GameStartObserverAction)startGameObserver).FirstPlayer.Username, 
                "FirstPlayer");
            Assert.AreEqual(((GameStartObserverAction)startGameObserver).SecondPlayer.Username, 
                "SecondPlayer");

        }

    }
}
