using System;

namespace GameData.Models.Units
{
    [Obsolete("Obsolte class. Use UnitState",false)]
    public class HealthPoint
    {

        public Unit Unit { private set; get; }

        public int BaseHealth { set; get; }

        public int RecievedDamage { private set; get; }

        public int GetResultHealth => BaseHealth - RecievedDamage;

        public event EventHandler<ZeroHpEventArgs> ZeroHpEvent;

        public event EventHandler<UnitRecievedDamageEventArgs> DamageRecieved; 

        public HealthPoint(Unit unit)
        {
            Unit = unit;
            RecievedDamage = 0;
            if(unit!=null)
                BaseHealth = Unit.BaseCard.BaseHP;
        }

        protected bool Equals(HealthPoint other)
        {
            return Equals(Unit, other.Unit) && BaseHealth == other.BaseHealth && RecievedDamage == other.RecievedDamage;
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
                hashCode = (hashCode * 397) ^ BaseHealth;
                hashCode = (hashCode * 397) ^ RecievedDamage;
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
            RecievedDamage += value;
            if(GetResultHealth <= 0)
                RunZeroHpEvent();

            RunDamageRecievedEvent(value);
        }

        public void Heal(int value)
        {
            if (RecievedDamage <= value)
                RecievedDamage = 0;
            else
                RecievedDamage -= value;
        }

        public override string ToString()
        {
            return GetResultHealth.ToString();
        }
    }
}
