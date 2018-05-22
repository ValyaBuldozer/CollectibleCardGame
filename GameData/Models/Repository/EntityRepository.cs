using System.Collections.Generic;
using Unity.Attributes;

namespace GameData.Models.Repository
{
    public class EntityRepository
    {
        [InjectionConstructor]
        public EntityRepository()
        {
            Collection = new List<Entity>();
        }

        public EntityRepository(IEnumerable<Entity> source)
        {
            Collection = new List<Entity>(source);
        }

        public List<Entity> Collection { set; get; }

        public void Reset()
        {
            Collection = new List<Entity>();
        }
    }
}