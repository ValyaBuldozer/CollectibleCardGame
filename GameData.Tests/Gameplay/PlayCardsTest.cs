using System.Linq;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models;
using GameData.Models.Cards;
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

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            //var observerRepository = container.Get<ObserverActionRepository>();
            //var startGameObserver =
            //    observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameStart);

            var player = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var card = player.HandCards.FirstOrDefault(c => c is UnitCard);
            var playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);


            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(3, ((CardDeployObserverAction) startGameObserver).Card.ID);
            Assert.AreEqual(1, container.Get<TableCondition>().Players.First().TableUnits.Count);
            Assert.AreEqual(5, container.Get<TableCondition>().Players.First().TableUnits[0].State.GetResultHealth);
        }

        [TestMethod]
        public void PlayUnitCardBattleCryTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var archerCard = firstDeck.FirstOrDefault(c => c.Name == "Лучник");
            var downKnightCard = firstDeck.FirstOrDefault(c => c.Name == "Павший рыцарь");
            firstDeck.Push(archerCard);
            firstDeck.Push(downKnightCard);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);


            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            var player = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var card1 = player.HandCards.FirstOrDefault(c => c.Name == "Лучник");
            var archerDeployTurn = new CardDeployPlayerTurn(player, card1);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(archerDeployTurn);
            var card2 = player.HandCards.FirstOrDefault(c => c.Name == "Павший рыцарь");
            var ditrixDelpoyTurn = new CardDeployPlayerTurn(player, card2);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(ditrixDelpoyTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var gameActionObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameAction);

            Assert.AreNotEqual(gameActionObserver, null);
            Assert.IsTrue(gameActionObserver is GameActionTriggerObserverAction action);
            Assert.AreEqual(1, ((GameActionTriggerObserverAction) gameActionObserver).GameActionId);
            Assert.AreEqual(2,
                player.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Лучник").State.GetResultHealth);
        }

        [TestMethod]
        public void PlayUnitCardDeathRattleTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard1 = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var knightDeployTurnP1 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard1);

            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(knightDeployTurnP1);

            var firstPlayerUnitCard2 = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Лекарь");
            var medicDeployTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard2);

            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(medicDeployTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var knightDeployTurnP2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(knightDeployTurnP2);
            firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Мечник").State.RecieveDamage(3);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(),
                firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Лекарь"));
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var gameActionObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameAction);

            Assert.AreNotEqual(gameActionObserver, null);
            Assert.IsTrue(gameActionObserver is GameActionTriggerObserverAction action);
            Assert.AreEqual(4, ((GameActionTriggerObserverAction) gameActionObserver).GameActionId);
            Assert.AreEqual(5,
                firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Мечник").State.GetResultHealth);
        }

        [TestMethod]
        public void PlayUnitCardAttackActionTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var knightDeployTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(knightDeployTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Лагерта");
            var lagertaDeployTurn = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(lagertaDeployTurn);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var gameActionObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameAction);

            Assert.AreNotEqual(gameActionObserver, null);
            Assert.IsTrue(gameActionObserver is GameActionTriggerObserverAction action);
            Assert.AreEqual(10, ((GameActionTriggerObserverAction) gameActionObserver).GameActionId);
            Assert.AreEqual(10, secondPlayer.TableUnits.First().State.Attack);
        }

        [TestMethod]
        public void PlayUnitCardDamageRecievedActionTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Дитрих Черный");
            var ditrihDeployTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(ditrihDeployTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var knightDeployTurn = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(knightDeployTurn);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var gameActionObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.GameAction);

            Assert.AreNotEqual(gameActionObserver, null);
            Assert.IsTrue(gameActionObserver is GameActionTriggerObserverAction action);
            Assert.AreEqual(10, ((GameActionTriggerObserverAction) gameActionObserver).GameActionId);
            Assert.AreEqual(8, firstPlayer.TableUnits.First().State.Attack);
        }


        [TestMethod]
        public void PlaySpellCardTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var player = container.Get<TableCondition>().Players.First();
            var card = player.HandCards.FirstOrDefault(c => c is SpellCard);
            var playerTurn = new CardDeployPlayerTurn(player, card);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            var observerRepository = container.Get<ObserverActionRepository>();
            var startGameObserver =
                observerRepository.Collection.FirstOrDefault(o => o.Type == ObserverActionType.CardDeploy);


            Assert.AreNotEqual(startGameObserver, null);
            Assert.IsTrue(startGameObserver is CardDeployObserverAction action);
            Assert.AreEqual(6, ((CardDeployObserverAction) startGameObserver).Card.ID);
        }


        [TestMethod]
        public void UnitHandleAttackTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var downKnightCard = secondDeck.FirstOrDefault(c => c.Name == "Павший рыцарь");
            secondDeck.Push(downKnightCard);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var playerTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Павший рыцарь");
            var playerTurn2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(0, firstPlayer.TableUnits.Count);
            Assert.AreEqual(1, secondPlayer.TableUnits.Count);
            Assert.AreEqual(5, secondPlayer.TableUnits.First().State.GetResultHealth);
        }

        [TestMethod]
        public void UnitHandleKillTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;
            var archerCard = secondDeck.FirstOrDefault(c => c.Name == "Лучник");
            secondDeck.Push(archerCard);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var playerTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(с => с.Name == "Лучник");
            var playerTurn2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(0, firstPlayer.TableUnits.Count);
            Assert.AreEqual(0, secondPlayer.TableUnits.Count);
        }

        [TestMethod]
        public void SpellComeInTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;
            var archerCard = secondDeck.FirstOrDefault(c => c.Name == "Лучник");
            secondDeck.Push(archerCard);
            var liderCard = firstDeck.FirstOrDefault(c => c.Name == "Лидер");
            firstDeck.Push(liderCard);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var playerTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(с => с.Name == "Лучник");
            var playerTurn2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            //формируем третий ход - спавн юнита первого игрока
            var thirdPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Лидер");
            var playerTurn3 = new CardDeployPlayerTurn(firstPlayer, thirdPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn3);


            Assert.AreEqual(6,
                firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Лидер").State.GetResultHealth);
            Assert.AreEqual(6,
                firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "Лидер").State.GetResultHealth);
        }

        [TestMethod]
        public void GiveCardWhenUnitDeployTest()
        {
            var testCards = new TestCards2();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;
            var archerCard = secondDeck.FirstOrDefault(c => c.Name == "Лучник");
            secondDeck.Push(archerCard);
            var liderCard = firstDeck.FirstOrDefault(c => c.Name == "Ученый");
            firstDeck.Push(liderCard);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Мечник");
            var playerTurn = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(с => с.Name == "Лучник");
            var playerTurn2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn2);

            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                secondPlayer, secondPlayer.TableUnits.First(), firstPlayer.TableUnits.First());
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();

            //формируем третий ход - спавн юнита первого игрока
            var countDeck = firstPlayer.State.DeckCardsCount;
            var thirdPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Ученый");
            var playerTurn3 = new CardDeployPlayerTurn(firstPlayer, thirdPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(playerTurn3);


            Assert.AreEqual(countDeck, firstPlayer.State.DeckCardsCount);
        }
    }
}