using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.PlayerTurn
{
    public class EndPlayerTurn : PlayerTurn
    {
        public EndPlayerTurn(Player sender)
        {
            Sender = sender;
            Type = PlayerTurnType.TurnEnd;
        }
    }
}
