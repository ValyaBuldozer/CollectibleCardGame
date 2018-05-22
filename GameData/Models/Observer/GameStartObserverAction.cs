using GameData.Enums;

namespace GameData.Models.Observer
{
    public class GameStartObserverAction : ObserverAction
    {
        public GameStartObserverAction(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            Type = ObserverActionType.GameStart;
        }

        public GameStartObserverAction()
        {
            Type = ObserverActionType.GameStart;
        }

        public Player FirstPlayer { set; get; }

        public Player SecondPlayer { set; get; }

        public string CurrentPlayerUsername { set; get; }
    }
}