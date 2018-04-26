using System.CodeDom;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Models.Units
{
    public class Unit
    {
        public UnitCard BaseCard { set; get; }

        public int Attack { set; get; }

        public HealthPoint HealthPoint { set; get; }

        public byte AttackPriority { set; get; }

        public bool CanAttack { set; get; }

        public Player Player { set; get; }

        public GameActionInfo BattleCryActionInfo { set; get; }

        public GameActionInfo DeathRattleActionInfo { set; get; }

        public GameActionInfo OnDamageRecievedActionInfo { set; get; }

        public GameActionInfo OnAttackActionInfo { set; get; }

        public Unit(UnitCard unitCard)
        {
            BaseCard = unitCard;
            Attack = BaseCard.BaseAttack;
            AttackPriority = BaseCard.AttackPriority;
            HealthPoint = new HealthPoint(this) { Base = BaseCard.BaseHP };
        }

        public override string ToString()
        {
            return BaseCard.Name;
        }
    }
}
