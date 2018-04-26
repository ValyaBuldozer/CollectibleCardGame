using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Repository;
using GameData.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameData.Tests.Controllers.UnitTests.Logic
{
    [TestClass]
    class GameActionTests
    {
        [TestMethod]
        public void DamageAllFriendlyUnitsTest() //тест екшена с id=1
        {
            //
            TestCards cards = new TestCards();


            Player p1 = new Player(cards.FirstCard);
            
            TestGameActionRepository testGameAction = new TestGameActionRepository();
            var actiionMock = new Mock<IGameActionController>();
            actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));
            //act
            UnitDispatcher dispatcher = new UnitDispatcher(actiionMock.Object);

            dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);
            dispatcher.CardPlayedSpawn(cards.AttackCard, p1, null);
            dispatcher.CardPlayedSpawn(cards.DefendCard, p1, null);
           
            //assert

        }
    }
}
