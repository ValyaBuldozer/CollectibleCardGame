using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class UnitSpawnObserverAction : Observer.ObserverAction
    {
        public Unit Unit { set; get; }

        public UnitSpawnObserverAction(Unit unit)
        {
            Unit = unit;
        }

        public UnitSpawnObserverAction() { }
    }
}
