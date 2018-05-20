using System.Collections.Generic;
using GameData.Models.Action;

namespace GameData.Models.Cards
{
    public class UnitCard : Card
    {
        public int BaseHP { set; get; }

        public int BaseAttack { set; get; }

        public byte AttackPriority { set; get; }

        public bool CanAttack { set; get; }

        public CardActionInfo AttackActionInfo { set; get; }

        public CardActionInfo DamageRecievedActionInfo { set; get; }

        public CardActionInfo BattleCryActionInfo { set; get; }

        public CardActionInfo DeathRattleActionInfo { set; get; }

        public override Card ShallowCopy()
        {
            return (Card)this.MemberwiseClone();
        }

        public override Card DeepCopy()
        {
            var other = (UnitCard) this.MemberwiseClone();

            other.AttackActionInfo = AttackActionInfo?.ShallowCopy();
            other.DamageRecievedActionInfo = DamageRecievedActionInfo?.ShallowCopy();
            other.BattleCryActionInfo = BattleCryActionInfo?.ShallowCopy();
            other.DeathRattleActionInfo = DeathRattleActionInfo?.ShallowCopy();

            return other;
        }
    }
}
