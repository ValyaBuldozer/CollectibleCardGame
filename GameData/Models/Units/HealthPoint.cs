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

        public void RecieveDamage(int value)
        {
            Damage += value;
            if(GetResult <= 0)
                RunZeroHpEvent();
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
