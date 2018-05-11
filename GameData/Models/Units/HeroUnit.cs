using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Models.Units
{
    public class HeroUnit : Unit
    {
        private HealthPoint _healthPoint;

        public override HealthPoint HealthPoint
        {
            set
            {
                if(_healthPoint != null)
                    _healthPoint.ZeroHpEvent -= HealthPoint_ZeroHpEvent;

                _healthPoint = value;
                _healthPoint.ZeroHpEvent += HealthPoint_ZeroHpEvent;
            }
            get => _healthPoint;
        }

        public event EventHandler<HeroUnitDiedEventArgs> DiedEvent; 

        public HeroUnit(Player player, UnitCard hero) : base(hero)
        {
            Player = player;

            if(HealthPoint!=null)
                 HealthPoint.ZeroHpEvent += HealthPoint_ZeroHpEvent;
        }

        private void HealthPoint_ZeroHpEvent(object sender, ZeroHpEventArgs e)
        {
            DiedEvent?.Invoke(this,new HeroUnitDiedEventArgs(Player));
        }
    }
}
