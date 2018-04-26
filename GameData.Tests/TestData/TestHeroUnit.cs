using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Tests.TestData
{
    public class TestHeroUnit
    {
        public HeroUnit First { set; get; }

        public HeroUnit Second { set; get; }

        public TestHeroUnit()
        {
            First = new HeroUnit(null,new UnitCard()
            {
                ID = 1,
                Name = "test1",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            });
            Second = new HeroUnit(null, new UnitCard()
            {
                ID = 2,
                Name = "test2",
                BaseHP = 40,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            });
        }
    }
}
