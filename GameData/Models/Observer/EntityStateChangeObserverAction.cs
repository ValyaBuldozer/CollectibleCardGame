using GameData.Enums;

namespace GameData.Models.Observer
{
    public class EntityStateChangeObserverAction : ObserverAction
    {
        public EntityStateChangeObserverAction(int id, Entity entity)
        {
            Type = ObserverActionType.EntityStateChange;
            EntityId = id;
            EntityState = entity;
        }

        public EntityStateChangeObserverAction()
        {
            Type = ObserverActionType.EntityStateChange;
        }

        public int EntityId { set; get; }

        public Entity EntityState { set; get; }
    }
}