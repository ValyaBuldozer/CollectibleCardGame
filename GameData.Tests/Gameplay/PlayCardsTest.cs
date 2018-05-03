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
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Models.Units;
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

            var player = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var card = player.HandCards.FirstOrDefault(c=>c is UnitCard);
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);

            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(3,((CardDeployObserverAction)startGameObserver).Card.ID);
            Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
            Assert.AreEqual(5, container.Get<TableCondition>().Players.First().TableUnits[0].HealthPoint.GetResult);

        }

        [TestMethod]
        public void PlaySpellCardTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var player = container.Get<TableCondition>().Players.First();
            var card = player.HandCards.FirstOrDefault(c => c is SpellCard);
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);

            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(6, ((CardDeployObserverAction)startGameObserver).Card.ID);
            //Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
            

        }


        [TestMethod]
        public void UnitHandleAttackTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c=>c is UnitCard);
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c=>c.Name == "Павший рыцарь");
            CardDeployPlayerTurn playerTurn2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            UnitAttackPlayerTurn unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(0, firstPlayer.TableUnits.Count);
            Assert.AreEqual(1, secondPlayer.TableUnits.Count);
            Assert.AreEqual(5, secondPlayer.TableUnits.First().HealthPoint.GetResult);

        }

        [TestMethod]
        public void UnitHandleKillTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Container container = new Container();
            container.Initialize();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var player1 = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
                
            var card1 = player1.HandCards.First();
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player1, card1);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var player2 = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var card2 = player2.HandCards[2];

            CardDeployPlayerTurn playerTurn2 = new CardDeployPlayerTurn(player2, card2);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            UnitAttackPlayerTurn unitAttackPlayerTurn = new  UnitAttackPlayerTurn(player1,player1.TableUnits.First(),player2.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(0, player1.TableUnits.Count);
            Assert.AreEqual(0, player2.TableUnits.Count);



        }

    }
}
