using System.CodeDom;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Models.Units
{
    public class Unit : Entity
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
            EntityType = EntityType.Unit;

            BaseCard = unitCard;
            Attack = BaseCard.BaseAttack;
            AttackPriority = BaseCard.AttackPriority;
            HealthPoint = new HealthPoint(this) { Base = BaseCard.BaseHP };
        }

        protected bool Equals(Unit other)
        {
            return BaseCard.ID == other.BaseCard.ID && Attack == other.Attack &&
                   Equals(HealthPoint, other.HealthPoint) && AttackPriority == other.AttackPriority &&
                   CanAttack == other.CanAttack && Equals(Player, other.Player) &&
                   Equals(BattleCryActionInfo, other.BattleCryActionInfo) &&
                   Equals(DeathRattleActionInfo, other.DeathRattleActionInfo) &&
                   Equals(OnDamageRecievedActionInfo, other.OnDamageRecievedActionInfo) &&
                   Equals(OnAttackActionInfo, other.OnAttackActionInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Unit) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (BaseCard != null ? BaseCard.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Attack;
                hashCode = (hashCode * 397) ^ (HealthPoint != null ? HealthPoint.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ AttackPriority.GetHashCode();
                hashCode = (hashCode * 397) ^ CanAttack.GetHashCode();
                hashCode = (hashCode * 397) ^ (Player != null ? Player.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BattleCryActionInfo != null ? BattleCryActionInfo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DeathRattleActionInfo != null ? DeathRattleActionInfo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OnDamageRecievedActionInfo != null ? OnDamageRecievedActionInfo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OnAttackActionInfo != null ? OnAttackActionInfo.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return BaseCard.Name;
        }
    }
}
