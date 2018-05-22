using GameData.Enums;

namespace GameData.Models.Observer
{
    public class GameActionTriggerObserverAction : ObserverAction
    {
        public GameActionTriggerObserverAction(int gameActionId, int senderEntityId, int targetEntityId)
        {
            Type = ObserverActionType.GameAction;
            GameActionId = gameActionId;
            SenderEntityId = senderEntityId;
            TargetEntityId = targetEntityId;
        }

        public GameActionTriggerObserverAction(int gameActionId, int senderEntityId, int? targetEntityId)
        {
            Type = ObserverActionType.GameAction;
            GameActionId = gameActionId;
            SenderEntityId = senderEntityId;

            if (targetEntityId != null)
                TargetEntityId = (int) targetEntityId;
        }

        public GameActionTriggerObserverAction()
        {
            Type = ObserverActionType.GameAction;
        }

        public int GameActionId { set; get; }

        public int SenderEntityId { set; get; }

        public int TargetEntityId { set; get; }
    }
}