using GameData.Enums;

namespace GameData.Models.Observer
{
    public class ErrorObserverAction : ObserverAction
    {
        public ErrorObserverAction(string errorMessage, Player targetPlayer)
        {
            TargetPlayer = targetPlayer;
            ErrorMessage = errorMessage;
            Type = ObserverActionType.Error;
        }

        public ErrorObserverAction()
        {
            Type = ObserverActionType.Error;
        }

        public string ErrorMessage { set; get; }

        public bool IsSystemError { set; get; }
    }
}