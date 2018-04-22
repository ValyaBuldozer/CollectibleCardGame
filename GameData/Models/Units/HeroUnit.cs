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
        public Player Player { get; }

        public event EventHandler<HeroUnitDiedEventArgs> DiedEvent; 

        public HeroUnit(Player player, UnitCard hero) : base(hero)
        {
            Player = player;
            HealthPoint.ZeroHpEvent += HealthPoint_ZeroHpEvent;
        }

        private void HealthPoint_ZeroHpEvent(object sender, ZeroHpEventArgs e)
        {
            DiedEvent?.Invoke(this,new HeroUnitDiedEventArgs(Player));
        }
    }
}
