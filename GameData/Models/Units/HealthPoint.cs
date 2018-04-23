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
