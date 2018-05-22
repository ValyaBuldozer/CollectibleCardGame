using GameData.Enums;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    /// <summary>
    ///     Изменение состояния юнита - получение урона, хил, бафф статистик
    /// </summary>
    public class UnitStateChangeObserverAction : ObserverAction
    {
        public UnitStateChangeObserverAction(Unit newUnit, int entityId)
        {
            Type = ObserverActionType.UnitStateChange;
            NewUnitState = newUnit;
            EntityId = entityId;
        }

        public UnitStateChangeObserverAction()
        {
            Type = ObserverActionType.UnitStateChange;
        }

        public Unit NewUnitState { set; get; }

        public int EntityId { set; get; }
    }
}