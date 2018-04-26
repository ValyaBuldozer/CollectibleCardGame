using System.Collections.Generic;
using System.Linq;
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

        public Player GetPlayerByUsername(string username)
        {
            return Players.FirstOrDefault(p => p.Username == username);
        }
    }
}
