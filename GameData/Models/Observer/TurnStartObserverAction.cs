using GameData.Enums;

namespace GameData.Models.Observer
{
    public class TurnStartObserverAction : ObserverAction
    {
        public TurnStartObserverAction(string playerUsername)
        {
            CurrentPlayerUsername = playerUsername;
            Type = ObserverActionType.TurnStart;
        }

        public TurnStartObserverAction()
        {
            Type = ObserverActionType.TurnStart;
        }

        public string CurrentPlayerUsername { set; get; }
    }
}