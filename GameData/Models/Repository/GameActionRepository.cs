using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Action;

namespace GameData.Models.Repository
{
    public class GameActionRepository
    {
        public List<GameAction> Collection { get; set; }

        public GameActionRepository()
        {
            Collection = new List<GameAction>()
            {

            };
        }
    }
}
