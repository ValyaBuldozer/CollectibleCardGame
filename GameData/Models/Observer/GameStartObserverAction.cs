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
        public string FirstPlayerUsername { set; get; }

        public string SecondPlayerUsername { set; get; }

        public GameStartObserverAction(string firstPlayer,string secondPlayer)
        {
            FirstPlayerUsername = firstPlayer;
            SecondPlayerUsername = secondPlayer;
            Type = ObserverActionType.GameStart;
        }

        public GameStartObserverAction()
        {
            Type = ObserverActionType.GameStart;
        }
    }
}
