using System.Collections.Generic;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Tests.TestData
{
    public class TestDeck
    {
        public Stack<Card> GetFirstDeck => new Stack<Card>(new List<Card>
        {
            Unit1_1,
            Unit3_3,
            FireballSpell,
            HealCard,
            Unit1_1,
            Unit3_3,
            Unit1_1,
            FireballSpell,
            FireballSpell
        });

        public UnitCard Unit1_1 => new UnitCard
        {
            ID = 1,
            Name = "Unit1_1",
            Cost = 1,
            CanBePlayedOnEnemyTurn = false,
            BaseAttack = 1,
            BaseHP = 1,
            AttackPriority = 1
        };

        public SpellCard FireballSpell => new SpellCard
        {
            ID = 2,
            Name = "Fireball",
            Cost = 1,
            CanBePlayedOnEnemyTurn = false,
            ActionInfo = new CardActionInfo
            {
                ActionId = 3,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            }
        };

        public UnitCard Unit3_3 => new UnitCard
        {
            ID = 3,
            Name = "Unit3_3",
            Cost = 3,
            BaseAttack = 3,
            BaseHP = 3,
            AttackPriority = 1,
            DeathRattleActionInfo = new CardActionInfo
            {
                ActionId = 5,
                ParameterValue = 1,
                ParameterType = ActionParameterType.Damage
            }
        };

        public SpellCard HealCard => new SpellCard
        {
            ID = 4,
            Name = "HealSpell",
            Cost = 2,
            CanBePlayedOnEnemyTurn = false,
            ActionInfo = new CardActionInfo
            {
                ActionId = 4,
                ParameterType = ActionParameterType.Heal,
                ParameterValue = 2
            }
        };
    }
}