using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Models.PlayerTurn
{
    public class CardDeployPlayerTurn : PlayerTurn
    {
        public Card Card { protected set; get; }

        public Unit ActionTarget { protected set; get; }

        public CardDeployPlayerTurn(Player sender, Card card, Unit target = null)
        {
            Sender = sender;
            Card = card;
            ActionTarget = target;
            Type = PlayerTurnType.CardDeploy;
        }
    }
}
