using GameData.Enums;

namespace GameData.Models.Observer
{
    public class GameEndObserverAction : ObserverAction
    {
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

        public string WinnerUsername { set; get; }

        public GameEndReason Reason { set; get; }
    }
}