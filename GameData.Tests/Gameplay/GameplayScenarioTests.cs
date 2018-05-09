using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models.Observer;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Gameplay
{
    [TestClass]
    public class GameplayScenarioTests
    {
        [TestMethod]
        public void GameplayScenatio_1()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);

            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            //container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            Assert.AreNotEqual(null, firstPlayer);
            Assert.AreNotEqual(null, secondPlayer);
            Assert.AreEqual(1,firstPlayer.Mana);
            Assert.AreEqual(0, secondPlayer.Mana);
            Assert.AreEqual(7,firstPlayer.DeckCardsCount);
            Assert.AreEqual(7, secondPlayer.DeckCardsCount);
            Assert.AreEqual(5,firstPlayer.HandCards.Count);
            Assert.AreEqual(5, secondPlayer.HandCards.Count);

        }

        [TestMethod]
        public void GameplayScenatio_2()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            CardDeployPlayerTurn knightDeployTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(knightDeployTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Лагерта");
            CardDeployPlayerTurn lagertaDeployTurn = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(lagertaDeployTurn);

            UnitAttackPlayerTurn unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var gameActionObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameAction);

            Assert.AreNotEqual(gameActionObserver, null);
            Assert.IsTrue(gameActionObserver is GameActionTriggerObserverAction action);
            Assert.AreEqual(10, ((GameActionTriggerObserverAction)gameActionObserver).GameActionId);
            Assert.AreEqual(10, secondPlayer.TableUnits.First().Attack);

        }


    }
}
