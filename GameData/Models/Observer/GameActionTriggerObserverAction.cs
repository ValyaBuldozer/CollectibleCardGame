using GameData.Models.Action;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class GameActionTriggerObserverAction : ObserverAction
    {
        public GameAction GameAction { set; get; }

        public Unit SenderUnit { set; get; }

        public GameActionTriggerObserverAction(GameAction gameAction, Unit sender = null)
        {
            GameAction = gameAction;
            SenderUnit = sender;
        }
    }
}
