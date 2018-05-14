using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class UnitSpawnObserverAction : Observer.ObserverAction
    {
        public string PlayerUsername { set; get; }

        public Unit Unit { set; get; }

        public UnitSpawnObserverAction(Unit unit,string playerUsername)
        {
            PlayerUsername = playerUsername;
            Unit = unit;
            Type = ObserverActionType.UnitSpawn;
        }

        public UnitSpawnObserverAction()
        {
            Type = ObserverActionType.UnitSpawn;
        }
    }
}
