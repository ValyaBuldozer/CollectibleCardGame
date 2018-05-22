using System.Collections.Generic;
using System.Linq;

namespace GameData.Models
{
    public class TableCondition
    {
        public TableCondition()
        {
            Players = new List<Player>();
        }

        public List<Player> Players { set; get; }

        public Player GetPlayerByUsername(string username)
        {
            return Players.FirstOrDefault(p => p.Username == username);
        }
    }
}