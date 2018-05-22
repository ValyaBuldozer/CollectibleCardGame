using GameData.Enums;

namespace GameData.Models
{
    public class Entity
    {
        public Entity(int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        public Entity()
        {
        }

        public int EntityId { set; get; }

        public EntityType EntityType { set; get; }

        protected bool Equals(Entity other)
        {
            return EntityId == other.EntityId && EntityType == other.EntityType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity) obj);
        }
    }
}