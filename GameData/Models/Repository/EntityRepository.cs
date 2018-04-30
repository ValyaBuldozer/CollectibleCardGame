using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.Repository
{
    public class EntityRepository
    {
        public List<Entity> Collection { set; get; }

        public EntityRepository()
        {
            Collection = new List<Entity>();
        }
    }
}
