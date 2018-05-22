using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class UnitDeathObserverAction : ObserverAction
    {
        public UnitDeathObserverAction(Unit unit)
        {
            Unit = unit;
            Type = ObserverActionType.UnitDeath;
        }

        public UnitDeathObserverAction()
        {
            Type = ObserverActionType.UnitDeath;
        }

        public Unit Unit { set; get; }
    }
}