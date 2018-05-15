using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.Observer
{
    public class GameEndObserverAction : ObserverAction
    {
        public string WinnerUsername { set; get; }

        public GameEndReason Reason { set; get; }

        public GameEndObserverAction(string winnerUsername, GameEndReason reason)
        {
            WinnerUsername = winnerUsername;
            Reason = reason;
            Type = ObserverActionType.GameEnd;
        }

        public GameEndObserverAction()
        {
            Type = ObserverActionType.GameEnd;
        }
    }
}
