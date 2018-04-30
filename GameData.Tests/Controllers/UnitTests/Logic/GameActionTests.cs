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
using GameData.Models.Cards;
using GameData.Models.Repository;
using GameData.Models.Units;
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
            //arrange
            TestCards cards = new TestCards();
           
            //cardDraw

            TableCondition tc = new TableCondition()
            {
                Players = new List<Player>()
                {
                    new Player(null)
                    {
                        Username = "Player1"
                    },
                    new Player(null)
                    {
                        Username = "Player2"
                    },
                }

            };

           Unit u1 = new Unit(cards.SecondCard);
           Unit u2 = new Unit(cards.AttackCard);
           Unit u3 = new Unit(cards.DefendCard);

            var actiionMock = new Mock<InActionTableController>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            GameActionController gaC = new GameActionController(new GameActionRepositoryController(new TestGameActionRepository()),
                    actiionMock.Object);
           
            gaC.GetGameActionInfo(new CardActionInfo()
            {
                ActionId = 1,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            });


            tc.Players[1].TableUnits.Add(u1);

            //var actiionMock = new Mock<IGameActionController>();
            //actiionMock.Setup(mock => mock.ExecuteAction(new GameActionInfo(), null, null));


            //act
            //UnitDispatcher dispatcher = new UnitDispatcher(actiionMock.Object);

            //dispatcher.CardPlayedSpawn(cards.SecondCard, p1, null);
            //dispatcher.CardPlayedSpawn(cards.AttackCard, p1, null);
            //dispatcher.CardPlayedSpawn(cards.DefendCard, p1, null);

            //dispatcher.GetUnit(cards.SecondCard);

            //assert

        }
    }
}
