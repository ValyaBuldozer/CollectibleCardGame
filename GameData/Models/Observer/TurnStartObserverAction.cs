using GameData.Enums;

namespace GameData.Models.Observer
{
    public class TurnStartObserverAction : Observer.ObserverAction
    {
        public string CurrentPlayerUsername { set; get; }

        public TurnStartObserverAction(string playerUsername)
        {
            CurrentPlayerUsername = playerUsername;
            Type = ObserverActionType.TurnStart;
        }

        public TurnStartObserverAction()
        {
            Type = ObserverActionType.TurnStart;
        }
    }
}
