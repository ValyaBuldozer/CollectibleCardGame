using System;
using System.Collections.Generic;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using Newtonsoft.Json;

namespace GameData.Models.Units
{
    public class Unit : Entity, IEquatable<Unit>
    {
        public Unit(UnitCard unitCard)
        {
            EntityType = EntityType.Unit;

            if (unitCard == null) return;

            BaseCard = unitCard;
            //Attack = BaseCard.BaseAttack;
            //AttackPriority = BaseCard.AttackPriority;
            //HealthPoint = new HealthPoint(this) { Base = BaseCard.BaseHP };
            State = new UnitState(this);
        }

        public UnitCard BaseCard { set; get; }

        public virtual UnitState State { set; get; }

        [JsonIgnore]
        public Player Player { set; get; }

        [JsonIgnore]
        public GameActionInfo BattleCryActionInfo { set; get; }

        [JsonIgnore]
        public GameActionInfo DeathRattleActionInfo { set; get; }

        [JsonIgnore]
        public GameActionInfo OnDamageRecievedActionInfo { set; get; }

        [JsonIgnore]
        public GameActionInfo OnAttackActionInfo { set; get; }

        public bool Equals(Unit other)
        {
            return other != null &&
                   base.Equals(other) &&
                   EqualityComparer<UnitCard>.Default.Equals(BaseCard, other.BaseCard) &&
                   EqualityComparer<UnitState>.Default.Equals(State, other.State) &&
                   EqualityComparer<Player>.Default.Equals(Player, other.Player) &&
                   EqualityComparer<GameActionInfo>.Default.Equals(BattleCryActionInfo, other.BattleCryActionInfo) &&
                   EqualityComparer<GameActionInfo>.Default.Equals(DeathRattleActionInfo,
                       other.DeathRattleActionInfo) &&
                   EqualityComparer<GameActionInfo>.Default.Equals(OnDamageRecievedActionInfo,
                       other.OnDamageRecievedActionInfo) &&
                   EqualityComparer<GameActionInfo>.Default.Equals(OnAttackActionInfo, other.OnAttackActionInfo);
        }

        public override string ToString()
        {
            return BaseCard.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Unit);
        }
    }
}