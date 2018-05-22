using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Cards;

namespace GameData.Models.Observer
{
    public class GameStartObserverAction : ObserverAction
    {
        public Player FirstPlayer { set; get; }

        public Player SecondPlayer { set; get; }

        public string CurrentPlayerUsername { set; get; }

        public GameStartObserverAction(Player firstPlayer,Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            Type = ObserverActionType.GameStart;
        }

        public GameStartObserverAction()
        {
            Type = ObserverActionType.GameStart;
        }
    }
}
