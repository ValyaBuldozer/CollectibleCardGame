using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Models.PlayerTurn
{
    public class CardPlayedPlayerTurn : IPlayerTurn
    {
        public Player Sender { set; get; }

        public ICard Card { set; get; }

        public object ActionTarget { set; get; }
    }
}
