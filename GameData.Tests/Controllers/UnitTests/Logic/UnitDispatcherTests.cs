using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameData.Tests.Controllers.UnitTests.Logic
{
    [TestClass]
    public class UnitDispatcherTests
    {
        [TestMethod]
        public void CardPlayedSpawnTest()
        {
            //arrange
            var cards = new TestCards();


            var p1 = new Player(cards.FirstCard);

            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));
            //act
            var dispatcher = new UnitDispatcher(actiionMock.Object, null, TestGameSettings.Get);
            dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);
            //assert
            actiionMock.Verify(mock => mock.ExecuteAction(It.IsAny<GameActionInfo>(), p1, null));
            Assert.AreNotEqual(p1.TableUnits[0], null);
            Assert.AreEqual(p1.TableUnits.Count, 1);
            Assert.AreEqual(p1.TableUnits[0].State.BaseHealth, 40);
            Assert.AreEqual(p1.TableUnits[0].State.Attack, 0);
        }

        [TestMethod]
        public void CardPlayedGoToTableTest()
        {
            //arrange
            var cards = new TestCards();


            var p1 = new Player(cards.FirstCard);

            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));
            //act
            var dispatcher = new UnitDispatcher(actiionMock.Object, null, TestGameSettings.Get);
            dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);

            //assert
            actiionMock.Verify(mock => mock.ExecuteAction(It.IsAny<GameActionInfo>(), p1, null));


            Assert.AreEqual(p1.TableUnits.Count, 1);
        }

        [TestMethod]
        public void KillTest()
        {
            //arrange
            var cards = new TestCards();


            var p1 = new Player(cards.FirstCard);

            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));
            //act
            var dispatcher = new UnitDispatcher(actiionMock.Object, null, TestGameSettings.Get);
            dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);
            dispatcher.Kill(p1.TableUnits[0]);

            //assert
            actiionMock.Verify(mock => mock.ExecuteAction(It.IsAny<GameActionInfo>(), p1, null));
            Assert.AreEqual(p1.TableUnits.Count, 0);
        }

        [TestMethod]
        public void SpawnTest()
        {
            //arrange
            var cards = new TestCards();


            var p1 = new Player(cards.FirstCard);


            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.GetGameActionInfo(new CardActionInfo()));

            //act
            var dispatcher = new UnitDispatcher(actiionMock.Object, null, TestGameSettings.Get);
            dispatcher.Spawn(cards.SecondCard, p1);

            //assert
            actiionMock.Verify(mock => mock.GetGameActionInfo(It.IsAny<CardActionInfo>()));
            Assert.AreNotEqual(p1.TableUnits[0], null);
            Assert.AreEqual(p1.TableUnits.Count, 1);
            Assert.AreEqual(p1.TableUnits[0].State.BaseHealth, 40);
            Assert.AreEqual(p1.TableUnits[0].State.Attack, 0);
        }

        [TestMethod]
        public void HandleAttackTest()
        {
            //arrange
            var cards = new TestCards();


            var p1 = new Player(cards.FirstCard);
            var p2 = new Player(cards.SecondCard);


            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));
            //act
            var dispatcher = new UnitDispatcher(actiionMock.Object, null, TestGameSettings.Get);
            dispatcher.CardPlayedSpawn(cards.AttackCard, p1, null);
            dispatcher.CardPlayedSpawn(cards.DefendCard, p2, null);
            dispatcher.HandleAttack(p1.TableUnits[0], p2.TableUnits[0]); // dont work here

            //assert
            actiionMock.Verify(mock => mock.ExecuteAction(It.IsAny<GameActionInfo>(), p1, null));
            actiionMock.Verify(mock => mock.ExecuteAction(It.IsAny<GameActionInfo>(), p2, null));
            Assert.AreEqual(p1.TableUnits[0].State.GetResultHealth, 5);
            Assert.AreEqual(p2.TableUnits[0].State.GetResultHealth, 5);
        }
    }
}