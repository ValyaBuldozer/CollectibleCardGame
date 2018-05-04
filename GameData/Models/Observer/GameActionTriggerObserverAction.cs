using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Units;

namespace GameData.Models.Observer
{
    public class GameActionTriggerObserverAction : ObserverAction
    {
        public int GameActionId { set; get; }

        public Entity Sender { set; get; }

        public Entity Target { set; get; }

        public GameActionTriggerObserverAction(int gameActionId, Entity sender, Entity target)
        {
            Type = ObserverActionType.GameAction;
            GameActionId = gameActionId;
            Sender = sender;
            Target = target;
        }

        public GameActionTriggerObserverAction()
        {
            Type = ObserverActionType.GameAction;
        }
    }
}
