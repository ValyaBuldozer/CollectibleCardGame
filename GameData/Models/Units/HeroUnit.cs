using System;
using GameData.Models.Cards;

namespace GameData.Models.Units
{
    public class HeroUnit : Unit
    {
        //private HealthPoint _healthPoint;
        private UnitState _state;

        public HeroUnit(Player player, UnitCard hero) : base(hero)
        {
            Player = player;

            if (State != null)
                State.ZeroHpEvent += HealthPoint_ZeroHpEvent;
        }

        //public override HealthPoint HealthPoint
        //{
        //    set
        //    {
        //        if(_healthPoint != null)
        //            _healthPoint.ZeroHpEvent -= HealthPoint_ZeroHpEvent;

        //        _healthPoint = value;
        //        _healthPoint.ZeroHpEvent += HealthPoint_ZeroHpEvent;
        //    }
        //    get => _healthPoint;
        //}

        public override UnitState State
        {
            get => _state;
            set
            {
                if (_state != null)
                    _state.ZeroHpEvent -= HealthPoint_ZeroHpEvent;

                _state = value;
                _state.ZeroHpEvent += HealthPoint_ZeroHpEvent;
            }
        }

        public event EventHandler<HeroUnitDiedEventArgs> DiedEvent;

        private void HealthPoint_ZeroHpEvent(object sender, ZeroHpEventArgs e)
        {
            DiedEvent?.Invoke(this, new HeroUnitDiedEventArgs(Player));
        }
    }
}