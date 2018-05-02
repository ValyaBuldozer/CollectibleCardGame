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
    class TestCards2
    {
        public UnitCard FirstCard { set; get; }

        public UnitCard SecondCard { set; get; }


        public TestCards2()
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
        }

        public Stack<Card> FirstRandomDeck => new Stack<Card>(new List<Card>()
        {
            new UnitCard()
            {
            ID = 3,
            Name = "Мечник",
            BaseHP = 5,
            AttackPriority = 1,
            BaseAttack = 5,
            Cost = 0,
            Description = "Test"
            },
            new UnitCard()
            {
                ID = 4,
                Name = "Лучник",
                BaseHP = 3,
                AttackPriority = 1,
                BaseAttack = 7,
                Cost = 0,
                Description = "Test"
            },
            new UnitCard()
            {
                ID = 5,
                Name = "Требушет",
                BaseHP = 2,
                AttackPriority = 1,
                BaseAttack = 8,
                Cost = 0,
                Description = "Test"
            },
            new SpellCard()
            {
                ID = 6,
                Name = "Огненный шар",
                Description = "Бахает всех врагов на 2",
                ActionInfo = new CardActionInfo()
                {
                    ActionId = 3, ParameterType = ActionParameterType.Damage,ParameterValue = 2
                }
            },
          
        });
    }
}
