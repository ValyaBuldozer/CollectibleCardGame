using System.Collections.Generic;
using GameData.Enums;

namespace GameData.Models
{
    public class TableCondition
    {
       public List<Player> Players { set; get; }

        public TableCondition()
        {
            Players = new List<Player>();
        }
    }
}
