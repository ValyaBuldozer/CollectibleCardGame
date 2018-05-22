using GameData.Enums;

namespace GameData.Models.Observer
{
    public class PlayerStateChangesObserverAction : ObserverAction
    {
        public PlayerStateChangesObserverAction(string username, PlayerState state)
        {
            PlayerState = state;
            PlayerUsername = username;
            Type = ObserverActionType.PlayerStateChange;
        }

        public PlayerStateChangesObserverAction()
        {
            Type = ObserverActionType.PlayerStateChange;
        }

        public string PlayerUsername { set; get; }

        public PlayerState PlayerState { set; get; }
    }
}