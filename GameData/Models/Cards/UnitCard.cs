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
    }
}
