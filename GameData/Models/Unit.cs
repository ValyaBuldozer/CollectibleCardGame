using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Action;
using GameData.Models.Cards;

namespace GameData.Models
{
    public class Unit
    {
        public UnitCard BaseCard { set; get; }

        public int Attack { set; get; }

        public HealthPoint HealthPoint { set; get; }

        public byte AttackPriority { set; get; }

        public bool CanAttack { set; get; }

        public GameActionInfo BattleCryActionInfo { set; get; }

        public GameActionInfo DeathRattleActionInfo { set; get; }

        public GameActionInfo OnDamageRecievedActionInfo { set; get; }

        public GameActionInfo OnAttackActionInfo { set; get; }


    }
}
