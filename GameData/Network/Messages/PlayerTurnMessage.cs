using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.PlayerTurn;

namespace GameData.Network.Messages
{
    public class PlayerTurnMessage : IContent
    {
        public PlayerTurn PlayerTurn { set; get; }

        public object AnswerData { set; get; }
    }
}
