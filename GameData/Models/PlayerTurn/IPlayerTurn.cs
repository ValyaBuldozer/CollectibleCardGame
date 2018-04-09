using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.PlayerTurn
{
    public interface IPlayerTurn
    {
        PlayerInfo Sender { set; get; }
    }
}
