using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.PlayerTurn
{
    public class EndPlayerTurn : IPlayerTurn
    {
        public PlayerInfo Sender { set; get; }
    }
}
