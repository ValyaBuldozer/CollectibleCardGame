using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.PlayerTurn
{
    public abstract class PlayerTurn
    {
        public Player Sender { protected set; get; }

        public PlayerTurnType Type { protected set; get; }
    }
}
