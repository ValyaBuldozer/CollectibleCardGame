using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Tests.TestData
{
    public class TestCards
    {
        public UnitCard FirstCard { set; get; }

        public UnitCard SecondCard { set; get; }

        public UnitCard AttackCard { set; get; }
        public UnitCard DefendCard { set; get; }

        public TestCards()
        {
            FirstCard = new UnitCard()
            {
                ID = 1,
                Name = "test1",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
            SecondCard = new UnitCard()
            {
                ID = 2,
                Name = "test2",
                BaseHP = 40,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
            AttackCard = new UnitCard()
            {
                ID = 3,
                Name = "test3",
                BaseHP = 10,
                AttackPriority = 1,
                BaseAttack =10,
                Cost = 0,
                Description = "Test"
            };
            DefendCard = new UnitCard()
            {
                ID = 4,
                Name = "test4",
                BaseHP = 15,
                AttackPriority = 1,
                BaseAttack = 5,
                Cost = 0,
                Description = "Test"
            };
        }
    }
}
