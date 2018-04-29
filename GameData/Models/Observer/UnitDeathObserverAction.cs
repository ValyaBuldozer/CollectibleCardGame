using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class UnitDeathObserverAction : Observer.ObserverAction
    {
        public Unit Unit { set; get; }

        public UnitDeathObserverAction(Unit unit)
        {
            Unit = unit;
        }

        public UnitDeathObserverAction() { }
    }
}
