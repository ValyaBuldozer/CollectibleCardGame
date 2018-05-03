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

            var player = container.Get<TableCondition>().Players.First();
            var card = player.HandCards.First();
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);

            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(3,((CardDeployObserverAction)startGameObserver).Card.ID);
            Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
            Assert.AreEqual(5, container.Get<TableCondition>().Players.First().TableUnits[0].HealthPoint);

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
            var card = player.HandCards[1];
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);

            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(6, ((CardDeployObserverAction)startGameObserver).Card.ID);
            Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
            

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


            var player1 = container.Get<TableCondition>().Players.First();
            var player2 = container.Get<TableCondition>().Players.FirstOrDefault(p => p.Username != player1.Username);

            var card1 = player1.HandCards.First();
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player1, card1);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var card2 = player2.HandCards[3];
            CardDeployPlayerTurn playerTurn2 = new CardDeployPlayerTurn(player2, card2);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            UnitAttackPlayerTurn playerTurn3 = new UnitAttackPlayerTurn(player1, player1.TableUnits.First(), player2.TableUnits.First());


            Assert.AreEqual(0, player1.TableUnits.Count);
            Assert.AreEqual(1, player2.TableUnits.Count);
            Assert.AreEqual(5, player2.TableUnits.First().HealthPoint);



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


            var player1 = container.Get<TableCondition>().Players.First();
            var player2 = container.Get<TableCondition>().Players.FirstOrDefault(p => p.Username != player1.Username);
                
            var card1 = player1.HandCards.First();
            CardDeployPlayerTurn playerTurn = new CardDeployPlayerTurn(player1, card1);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var card2 = player2.HandCards[2];
            CardDeployPlayerTurn playerTurn2 = new CardDeployPlayerTurn(player2, card2);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            UnitAttackPlayerTurn playerTurn3 = new  UnitAttackPlayerTurn(player1,player1.TableUnits.First(),player2.TableUnits.First());


            Assert.AreEqual(0, player1.TableUnits.Count);
            Assert.AreEqual(0, player2.TableUnits.Count);



        }

    }
}
