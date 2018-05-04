using GameData.Enums;

namespace GameData.Models.Observer
{
    public class TurnStartObserverAction : Observer.ObserverAction
    {
        public Player CurrentPlayer { set; get; }

        public TurnStartObserverAction(Player player)
        {
            CurrentPlayer = player;
            Type = ObserverActionType.TurnStart;
        }

        public TurnStartObserverAction()
        {
            Type = ObserverActionType.TurnStart;
        }
    }
}
