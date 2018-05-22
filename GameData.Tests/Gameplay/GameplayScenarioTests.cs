using System.Linq;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Kernel;
using GameData.Models;
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
        public void GameplayScenario_1()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            Assert.AreEqual(9, firstDeck.Count);
            Assert.AreEqual(9, secondDeck.Count);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);

            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            Assert.AreNotEqual(null, firstPlayer);
            Assert.AreNotEqual(null, secondPlayer);
            Assert.AreEqual(1, firstPlayer.State.Current);
            Assert.AreEqual(1, secondPlayer.State.Current);
            Assert.AreEqual(4, firstDeck.Count);
            Assert.AreEqual(4, secondDeck.Count);
            Assert.AreEqual(5, firstPlayer.HandCards.Count);
            Assert.AreEqual(5, secondPlayer.HandCards.Count);
        }

        [TestMethod]
        public void GameplayScenario_5()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;
            var card1 = firstDeck.FirstOrDefault(c => c.Name == "UnitCard1_5/5");
            var card2 = secondDeck.FirstOrDefault(c => c.Name == "UnitCard2_3/6");
            firstDeck.Push(card1);
            secondDeck.Push(card2);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard1_5/5");
            var deployTurnUnit1 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit1);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard2_3/6");
            var deployTurnUnit2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit2);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();


            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                firstPlayer, firstPlayer.TableUnits.First(), secondPlayer.HeroUnit);
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);


            Assert.AreEqual(1, firstPlayer.TableUnits.Count);
            Assert.AreEqual(5, firstPlayer.TableUnits.FirstOrDefault().State.Attack);
            Assert.AreEqual(5, firstPlayer.TableUnits.FirstOrDefault().State.GetResultHealth);
            Assert.AreEqual(3, secondPlayer.TableUnits.FirstOrDefault().State.Attack);
            Assert.AreEqual(6, secondPlayer.TableUnits.FirstOrDefault().State.GetResultHealth);
            Assert.AreEqual(1, secondPlayer.TableUnits.Count);
            Assert.AreEqual(25, secondPlayer.HeroUnit.State.GetResultHealth);
        }

        [TestMethod]
        public void GameplayScenario_9()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard6_6/6");
            var deployTurnUnit6 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit6);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard1_5");
            var deployTurnSpell_1 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_1);


            Assert.AreEqual(1, firstPlayer.TableUnits.FirstOrDefault().State.GetResultHealth);
            Assert.AreEqual(6, firstPlayer.TableUnits.FirstOrDefault().State.Attack);
        }

        [TestMethod]
        public void GameplayScenario_10()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard4_4/4");
            var deployTurnUnit4 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit4);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard1_5");
            var deployTurnSpell_1 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_1);


            Assert.AreEqual(0, firstPlayer.TableUnits.Count);
        }

        [TestMethod]
        public void GameplayScenario_11()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerSpellCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard2_5");
            var deployTurnSpell_2 = new CardDeployPlayerTurn(firstPlayer, firstPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_2);


            Assert.AreEqual(25, container.Get<TableCondition>().Players.FirstOrDefault(
                c => c.Username == "SecondPlayer").HeroUnit.State.GetResultHealth);
        }


        [TestMethod]
        public void GameplayScenario_12()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            var observer = container.Get<ObserverActionRepository>();
            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var secondPlayer = container.Get<TableCondition>().GetPlayerByUsername("SecondPlayer");
            secondPlayer.HeroUnit.State.BaseHealth = 5;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard5_6/4");
            var deployTurnUnit4 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit4);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard6_6/6");
            var deployTurnUnit_6 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit_6);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            //формируем третий ход 
            var unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                firstPlayer, firstPlayer.TableUnits.First(), secondPlayer.HeroUnit);
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(-1, secondPlayer.HeroUnit.State.GetResultHealth);
        }

        [TestMethod]
        public void GameplayScenario_13()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            var container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            var secondPlayer = container.Get<TableCondition>().GetPlayerByUsername("SecondPlayer");


            secondPlayer.HeroUnit.State.BaseHealth = 5;
            //формируем первый ход 
            var firstPlayerSpellCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard2_5");
            var deployTurnSpell_2 = new CardDeployPlayerTurn(firstPlayer, firstPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_2);


            Assert.AreEqual(0, secondPlayer.HeroUnit.State.GetResultHealth);
        }

        [TestMethod]
        public void GameplayScenario_17()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;

            var card2 = secondDeck.FirstOrDefault(c => c.Name == "SpellCard3_5");
            secondDeck.Push(card2);

            var container = new Container();
            container.Initialize(TestGameSettings.Get);
            var observerRepository = container.Get<ObserverActionRepository>();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            //формируем первый ход 
            var firstPlayerUnitCard1 = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard6_6/6");
            var deployTurnUnit6 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard1);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit6);
            var firstPlayerUnitCard2 = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard5_6/4");
            var deployTurnUnit5 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard2);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit5);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard3_5");
            var deployTurnSpell_3 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_3);


            var ORfirst = firstPlayer.TableUnits.FirstOrDefault(c => c.BaseCard.Name == "UnitCard6_6/6").State
                              .GetResultHealth == 1;
            var ORtsecond = firstPlayer.TableUnits.Count == 1;
            Assert.IsTrue(ORfirst || ORtsecond);
        }
    }
}