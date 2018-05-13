using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.Observer
{
    public class EntityStateChangeObserverAction : ObserverAction
    {
        public int EntityId { set; get; }

        public Entity EntityState { set; get; }

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
    }
}
