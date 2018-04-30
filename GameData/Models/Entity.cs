using GameData.Enums;

namespace GameData.Models
{
    public class Entity
    {
        public int EntityId { set; get; }

        public EntityType EntityType { set; get; }

        public Entity(int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        public Entity() { }
    }
}
