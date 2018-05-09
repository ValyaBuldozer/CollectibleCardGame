using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace GameData.Models.Repository
{
    public class EntityRepository
    {
        public List<Entity> Collection { set; get; }

        [InjectionConstructor]
        public EntityRepository()
        {
            Collection = new List<Entity>();
        }

        public EntityRepository(IEnumerable<Entity> source)
        {
            Collection = new List<Entity>(source);
        }

        public void Reset()
        {
            Collection = new List<Entity>();
        }
    }
}
