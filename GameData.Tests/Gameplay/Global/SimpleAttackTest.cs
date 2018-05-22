using System.Linq;
using GameData.Controllers.Global;
using GameData.Controllers.PlayerTurn;
using GameData.Enums;
using GameData.Kernel;
using GameData.Models;
using GameData.Models.PlayerTurn;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameData.Tests.Gameplay.Global
{
    [TestClass]
    public class SimpleAttackTest
    {
        [TestMethod]
        public void TwoUnitsSpawnAndAttackHero()
        {
            var testCards = new TestCards();
            var testDeck = new TestDeck();
            var firstDeck = testDeck.GetFirstDeck;
            var secondDeck = testDeck.GetFirstDeck;

            var container = new Container();
            container.Initialize(TestGameSettings.Get);
            var observerRepository = container.Get<ObserverActionRepository>();
            var turnDispatcher = container.Get<IPlayerTurnDispatcher>();
            var cardDeployHandler = container.Get<IPlayerTurnHandler<CardDeployPlayerTurn>>();
            var turnEndHandler = container.Get<IPlayerTurnHandler<EndPlayerTurn>>();
            var attackHandler = container.Get<IPlayerTurnHandler<UnitAttackPlayerTurn>>();

            container.Get<IGameStateController>().Start(firstDeck, "FirstPlayer", testCards.FirstCard,
                secondDeck, "SecondPlayer", testCards.SecondCard);

            //спавним 2 юнитов у первого игрока
            var firstPlayer = turnDispatcher.CurrentPlayer;
            var unit1_1card = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Unit1_1");
            var unit3_3card = firstPlayer.HandCards.FirstOrDefault(c => c.Name == "Unit3_3");

            var unit11CardDeploy = new CardDeployPlayerTurn(firstPlayer, unit1_1card);
            var unit33CardDeploy = new CardDeployPlayerTurn(firstPlayer, unit3_3card);

            firstPlayer.State.Base = 7;
            firstPlayer.State.Restore();

            cardDeployHandler.Execute(unit11CardDeploy);
            cardDeployHandler.Execute(unit33CardDeploy);

            Assert.AreEqual(2, firstPlayer.TableUnits.Count);
            Assert.AreEqual(3, firstPlayer.State.Current);
            Assert.AreEqual(2, observerRepository.Collection.Count(
                o => o.Type == ObserverActionType.CardDeploy));

            //смена хода
            var turnSkip = new EndPlayerTurn(firstPlayer);
            turnEndHandler.Execute(turnSkip);
            var secondPlayer = turnDispatcher.CurrentPlayer;

            Assert.AreNotEqual(turnDispatcher.CurrentPlayer, firstPlayer);
            Assert.AreEqual(2, observerRepository.Collection.Count(
                o => o.Type == ObserverActionType.TurnStart));

            unit1_1card = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Unit1_1");
            unit3_3card = secondPlayer.HandCards.FirstOrDefault(c => c.Name == "Unit3_3");

            unit11CardDeploy = new CardDeployPlayerTurn(secondPlayer, unit1_1card);
            unit33CardDeploy = new CardDeployPlayerTurn(secondPlayer, unit3_3card);

            secondPlayer.State.Base = 7;
            secondPlayer.State.Restore();

            cardDeployHandler.Execute(unit11CardDeploy);
            cardDeployHandler.Execute(unit33CardDeploy);

            Assert.AreEqual(2, secondPlayer.TableUnits.Count);
            Assert.AreEqual(3, secondPlayer.State.Current);
            Assert.AreEqual(4, observerRepository.Collection.Count(
                o => o.Type == ObserverActionType.CardDeploy));

            turnSkip = new EndPlayerTurn(secondPlayer);
            turnEndHandler.Execute(turnSkip);
            firstPlayer = turnDispatcher.CurrentPlayer;

            Assert.AreNotEqual(turnDispatcher.CurrentPlayer, secondPlayer);
            Assert.AreEqual(3, observerRepository.Collection.Count(
                o => o.Type == ObserverActionType.TurnStart));

            var enemyPlayer = container.Get<TableCondition>().Players.Find
                (p => p.Username != firstPlayer.Username);
            var senderAttackUnit = firstPlayer.TableUnits.Find(u => u.State.GetResultHealth == 3);
            var targetAttackUnit = enemyPlayer.TableUnits.Find(u => u.State.GetResultHealth == 1);

            var attackPlayerTurn = new UnitAttackPlayerTurn(
                firstPlayer, senderAttackUnit, targetAttackUnit);
            attackHandler.Execute(attackPlayerTurn);

            Assert.AreEqual(1, observerRepository.Collection.Count(
                o => o.Type == ObserverActionType.UnitDeath));
            Assert.AreEqual(1, enemyPlayer.TableUnits.Count);
            Assert.AreEqual(2, senderAttackUnit.State.GetResultHealth);
        }
    }
}