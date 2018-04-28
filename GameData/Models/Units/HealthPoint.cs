using System;

namespace GameData.Models.Units
{
    public class HealthPoint
    {

        public Unit Unit { private set; get; }

        public int Base { set; get; }

        public int Damage { private set; get; }

        public int GetResult => Base - Damage;

        public event EventHandler<ZeroHpEventArgs> ZeroHpEvent;

        public event EventHandler<UnitRecievedDamageEventArgs> DamageRecieved; 

        public HealthPoint(Unit unit)
        {
            Unit = unit;
            Damage = 0;
            Base = Unit.BaseCard.BaseHP;
        }

        protected bool Equals(HealthPoint other)
        {
            return Equals(Unit, other.Unit) && Base == other.Base && Damage == other.Damage;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HealthPoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Unit != null ? Unit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Base;
                hashCode = (hashCode * 397) ^ Damage;
                return hashCode;
            }
        }

        private void RunZeroHpEvent()
        {
            ZeroHpEvent?.Invoke(Unit,new ZeroHpEventArgs(Unit));
        }

        private void RunDamageRecievedEvent(int damage)
        {
            DamageRecieved?.Invoke(Unit,new UnitRecievedDamageEventArgs(Unit,damage));
        }

        public void RecieveDamage(int value)
        {
            if(value == 0)
                return;
            Damage += value;
            if(GetResult <= 0)
                RunZeroHpEvent();

            RunDamageRecievedEvent(value);
        }

        public void Heal(int value)
        {
            if (Damage <= value)
                Damage = 0;
            else
                Damage -= value;
        }
    }
}
