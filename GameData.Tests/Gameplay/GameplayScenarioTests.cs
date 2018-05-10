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

            Assert.AreEqual(8,firstDeck.Count);
            Assert.AreEqual(8, secondDeck.Count);

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);

            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;


            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            Assert.AreNotEqual(null, firstPlayer);
            Assert.AreNotEqual(null, secondPlayer);
            Assert.AreEqual(1,firstPlayer.Mana.Current);
            Assert.AreEqual(1, secondPlayer.Mana.Current);
            Assert.AreEqual(3,firstDeck.Count);
            Assert.AreEqual(3, secondDeck.Count);
            Assert.AreEqual(5,firstPlayer.HandCards.Count);
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

            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard1_5/5");
            CardDeployPlayerTurn deployTurnUnit1 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit1);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход - спавн второго игрока
            var secondPlayerUnitCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard2_3/6");
            CardDeployPlayerTurn deployTurnUnit2 = new CardDeployPlayerTurn(secondPlayer, secondPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit2);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
           

            UnitAttackPlayerTurn unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                firstPlayer, firstPlayer.TableUnits.First(), secondPlayer.HeroUnit);
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

           
            Assert.AreEqual(1,firstPlayer.TableUnits.Count);
            Assert.AreEqual(5, firstPlayer.TableUnits.FirstOrDefault().Attack);
            Assert.AreEqual(5, firstPlayer.TableUnits.FirstOrDefault().HealthPoint.GetResult);
            Assert.AreEqual(3,secondPlayer.TableUnits.FirstOrDefault().Attack);
            Assert.AreEqual(6, secondPlayer.TableUnits.FirstOrDefault().HealthPoint.GetResult);
            Assert.AreEqual(1, secondPlayer.TableUnits.Count);
            Assert.AreEqual(25,secondPlayer.HeroUnit.HealthPoint.GetResult);


        }

        [TestMethod]
        public void GameplayScenario_9()
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
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard6_6/6");
            CardDeployPlayerTurn deployTurnUnit6 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit6);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard1_5");
            CardDeployPlayerTurn deployTurnSpell_1 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_1);
            

            Assert.AreEqual(1, firstPlayer.TableUnits.FirstOrDefault().HealthPoint.GetResult);
            Assert.AreEqual(6, firstPlayer.TableUnits.FirstOrDefault().Attack);


        }

        [TestMethod]
        public void GameplayScenario_10()
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
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard4_4/4");
            CardDeployPlayerTurn deployTurnUnit4 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit4);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            var secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard1_5");
            CardDeployPlayerTurn deployTurnSpell_1 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_1);


            Assert.AreEqual(0, firstPlayer.TableUnits.Count);
            


        }

        [TestMethod]
        public void GameplayScenario_11()
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
            var firstPlayerSpellCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard2_5");
            CardDeployPlayerTurn deployTurnSpell_2 = new CardDeployPlayerTurn(firstPlayer, firstPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_2);

           


            Assert.AreEqual(25, container.Get<TableCondition>().Players.FirstOrDefault(c => c.Username == "SecondPlayer").HeroUnit.HealthPoint.GetResult);



        }


        [TestMethod]
        public void GameplayScenario_12()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;
            var secondPlayer = container.Get<TableCondition>().GetPlayerByUsername("SecondPlayer");
            secondPlayer.HeroUnit.HealthPoint.Base = 5;

            //формируем первый ход - спавн юнита первого игрока
            var firstPlayerUnitCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard5_6/4");
            CardDeployPlayerTurn deployTurnUnit4 = new CardDeployPlayerTurn(firstPlayer, firstPlayerUnitCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnUnit4);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            secondPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            //формируем второй ход 
            var secondPlayerSpellCard = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "UnitCard6_6/6");
            CardDeployPlayerTurn deployTurnSpell_1 = new CardDeployPlayerTurn(secondPlayer, secondPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_1);

            //передаем ход
            container.Get<IPlayerTurnDispatcher>().NextPlayer();
            //формируем третий ход 
            UnitAttackPlayerTurn unitAttackPlayerTurn = new UnitAttackPlayerTurn(
                firstPlayer, firstPlayer.TableUnits.First(), secondPlayer.HeroUnit);
            container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>().Execute(unitAttackPlayerTurn);

            Assert.AreEqual(-1, secondPlayer.HeroUnit.HealthPoint.GetResult);



        }

        [TestMethod]
        public void GameplayScenario_13()
        {
            var testCards = new TestCards3();
            var firstDeck = testCards.FirstRandomDeck;
            var secondDeck = testCards.FirstRandomDeck;


            Container container = new Container();
            container.Initialize(TestGameSettings.Get);

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstHero,
                secondDeck, "SecondPlayer", testCards.SecondHero);


            var firstPlayer = container.Get<IPlayerTurnDispatcher>().CurrentPlayer;

            var secondPlayer = container.Get<TableCondition>().GetPlayerByUsername("SecondPlayer");


            secondPlayer.HeroUnit.HealthPoint.Base = 5;
            //формируем первый ход 
            var firstPlayerSpellCard = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "SpellCard2_5");
            CardDeployPlayerTurn deployTurnSpell_2 = new CardDeployPlayerTurn(firstPlayer, firstPlayerSpellCard);
            container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>().Execute(deployTurnSpell_2);




            Assert.AreEqual(0, secondPlayer.HeroUnit.HealthPoint.GetResult);
           



        }


    }
}
