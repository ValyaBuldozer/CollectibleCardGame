using System.Collections.Generic;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Action;
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
        public void CallDamageAllFriendlyUnitsTest() //тест екшена с id=1
        {
            //arrange
            var cards = new TestCards();

            //cardDraw


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 1,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            });


            Assert.AreEqual(result.Action.Name, "DamageAllFriendlyUnits");
            Assert.AreEqual(result.Action.ID, 1);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Damage);
        }

        [TestMethod]
        public void CallBuffDamageSpellCardsTest() //тест екшена с id=2
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 2,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Buff
            });

            Assert.AreEqual(result.Action.Name, "BuffDamageSpellCards");
            Assert.AreEqual(result.Action.ID, 2);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Buff);
        }

        [TestMethod]
        public void CallDamageAllEnemyUnitsTest() //тест екшена с id=3
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 3,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            });

            Assert.AreEqual(result.Action.Name, "DamageAllEnemyUnits");
            Assert.AreEqual(result.Action.ID, 3);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Damage);
        }

        [TestMethod]
        public void CallHealAllFriendlyUnitsTest() //тест екшена с id=4
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 4,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Heal
            });

            Assert.AreEqual(result.Action.Name, "HealAllFriendlyUnits");
            Assert.AreEqual(result.Action.ID, 4);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Heal);
        }

        [TestMethod]
        public void CallDamageAllUnitsTest() //тест екшена с id=5
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 5,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            });

            Assert.AreEqual(result.Action.Name, "DamageAllUnits");
            Assert.AreEqual(result.Action.ID, 5);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Damage);
        }

        [TestMethod]
        public void CallBuffAttackFriendlyUnitsTest() //тест екшена с id=6
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 6,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Buff
            });

            Assert.AreEqual(result.Action.Name, "BuffAttackFriendlyUnits");
            Assert.AreEqual(result.Action.ID, 6);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Buff);
        }

        [TestMethod]
        public void CallFullBuffFriendlyUnitsTest() //тест екшена с id=7
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 7,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Buff
            });

            Assert.AreEqual(result.Action.Name, "FullBuffFriendlyUnits");
            Assert.AreEqual(result.Action.ID, 7);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Buff);
        }

        [TestMethod]
        public void CallDamageRandomEnemyUnitTest() //тест екшена с id=8
        {
            //arrange
            var cards = new TestCards();


            var u2 = new Unit(cards.AttackCard);
            var u3 = new Unit(cards.DefendCard);

            var tc = new TableCondition
            {
                Players = new List<Player>
                {
                    new Player(cards.FirstCard)
                    {
                        Username = "Player1"
                    },
                    new Player(cards.SecondCard)
                    {
                        Username = "Player2"
                    }
                }
            };


            var actiionMock = new Mock<IActionTableControlller>();
            actiionMock.Setup(mock => mock.GetTableCondition).Returns(tc);

            IGameActionController gaC = new GameActionController(
                new GameActionRepositoryController(new TestGameActionRepository()),
                actiionMock.Object);

            var result = gaC.GetGameActionInfo(new CardActionInfo
            {
                ActionId = 8,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            });

            Assert.AreEqual(result.Action.Name, "DamageRandomEnemyUnit");
            Assert.AreEqual(result.Action.ID, 8);
            Assert.AreEqual(result.Action.Description, "test");
            Assert.AreEqual(result.Action.ParameterType, ActionParameterType.Damage);
        }
    }
}