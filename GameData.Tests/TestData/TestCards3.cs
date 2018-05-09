using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Tests.TestData
{
    class TestCards3
    {
        public UnitCard FirstHero { set; get; }

        public UnitCard SecondHero { set; get; }


        public TestCards3()
        {
            FirstHero = new UnitCard()
            {
                ID = 1,
                Name = "Hero1",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
            SecondHero = new UnitCard()
            {
                ID = 2,
                Name = "Hero2",
                BaseHP = 30,
                AttackPriority = 1,
                BaseAttack = 0,
                Cost = 0,
                Description = "Test"
            };
        }

        public Stack<Card> FirstRandomDeck => new Stack<Card>(new List<Card>()
        {
            new UnitCard()
            {
                ID = 1,
                Name = "UnitCard1",
                BaseHP = 5,
                AttackPriority = 1,
                BaseAttack = 5,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard()
            {
                ID = 2,
                Name = "UnitCard2",
                BaseHP = 6,
                AttackPriority = 1,
                BaseAttack = 3,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard()
            {
                ID = 3,
                Name = "UnitCard3",
                BaseHP = 5,
                AttackPriority = 1,
                BaseAttack = 4,
                Cost = 0,
                Description = "Test",
               
            },
            new UnitCard()
            {
                ID = 4,
                Name = "UnitCard4",
                BaseHP = 4,
                AttackPriority = 1,
                BaseAttack = 4,
                Cost = 0,
                Description = "Test",
               
            },
            new UnitCard()
            {
                ID = 5,
                Name = "UnitCard5",
                BaseHP = 4,
                AttackPriority = 1,
                BaseAttack = 6,
                Cost = 0,
                Description = "Test",
                
            },
            new UnitCard()
            {
                ID = 6,
                Name = "UnitCard6",
                BaseHP = 6,
                AttackPriority = 1,
                BaseAttack = 6,
                Cost = 0,
                Description = "Test",
               
            },
            new SpellCard()
            {
                ID = 7,
                Name = "CardDamageSpell",
                Description = "Бахает всех врагов на 6",
                ActionInfo = new CardActionInfo()
                {
                    ActionId = 3, ParameterType = ActionParameterType.Damage,ParameterValue = 6
                }
            },
           

        });
    }
}
