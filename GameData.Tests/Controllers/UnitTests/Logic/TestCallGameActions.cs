using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Cards;
using GameData.Models.Units;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameData.Tests.Controllers.UnitTests.Logic
{
    [TestClass]
    public class TestCallGameActions
    {
        [TestMethod]
        public void DamageAllFriendlyUnitsTest()
        {
            TestCards cards = new TestCards();
            Player p1 = new Player(cards.FirstCard);
            Player p2 = new Player(cards.SecondCard);
            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new TestGameActionRepository().Collection[0], null, null));
            //act
            UnitDispatcher dispatcher = new UnitDispatcher(actiionMock.Object);
            Unit u1 = dispatcher.GetUnit(cards.AttackCard);
            //u1.BattleCryActionInfo.Action.ID=1
            dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);
        }
    }
}
