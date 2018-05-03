using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class UnitDeathObserverAction : Observer.ObserverAction
    {
        public Unit Unit { set; get; }

        public UnitDeathObserverAction(Unit unit)
        {
            Unit = unit;
            Type = ObserverActionType.UnitDeath;
        }

        public UnitDeathObserverAction()
        {
            Type = ObserverActionType.UnitDeath;
        }
    }
}
