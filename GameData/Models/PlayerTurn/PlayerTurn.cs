using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using Newtonsoft.Json;

namespace GameData.Models.PlayerTurn
{
    public abstract class PlayerTurn
    {
        [JsonIgnore]
        public Player Sender { set; get; }

        public PlayerTurnType Type { protected set; get; }
    }
}
