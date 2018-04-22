using System.Collections.Generic;

namespace GameData.Models.Cards
{
    public class UnitCard : ICard
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public int Cost { set; get; }

        public bool CanBePlayedOnEnemyTurn { set; get; }

        public int BaseHP { set; get; }

        public int BaseAttack { set; get; }

        public byte AttackPriority { set; get; }

        public int AttackActionId { set; get; }

        public int DamageRecievedActionId { set; get; }

        public int BattleCryActionId { set; get; }

        public int DeathRattleActionId { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
