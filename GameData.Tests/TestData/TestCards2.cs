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
                ID = 5,
                Name = "Требушет",
                BaseHP = 2,
                AttackPriority = 1,
                BaseAttack = 8,
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
                ID = 7,
                Name = "Павший рыцарь",
                BaseHP = 10,
                AttackPriority = 1,
                BaseAttack = 10,
                Cost = 0,
                Description = "Test",
                BattleCryActionInfo = new CardActionInfo() {ActionId = 1,ParameterType = ActionParameterType.Damage,ParameterValue = 1}
            },
            new UnitCard()
            {
                ID = 8,
                Name = "Лекарь",
                BaseHP = 2,
                AttackPriority = 1,
                BaseAttack = 4,
                Cost = 0,
                Description = "Test",
                DeathRattleActionInfo = new CardActionInfo() {ActionId = 4, ParameterType = ActionParameterType.Heal, ParameterValue = 3}
            },
            new UnitCard()
            {
                ID = 9,
                Name = "Лагерта",
                BaseHP = 7,
                AttackPriority = 1,
                BaseAttack = 8,
                Cost = 0,
                Description = "Test",
                AttackActionInfo = new CardActionInfo(){ActionId = 10,ParameterType = ActionParameterType.Buff,ParameterValue = 2}
            },
            new UnitCard()
            {
                ID = 10,
                Name = "Дитрих Черный",
                BaseHP = 9,
                AttackPriority = 1,
                BaseAttack = 6,
                Cost = 0,
                Description = "Test",
                DamageRecievedActionInfo = new CardActionInfo(){ActionId = 10,ParameterType = ActionParameterType.Buff,ParameterValue = 2}
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

        });
    }
}
