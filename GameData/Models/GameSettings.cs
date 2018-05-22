namespace GameData.Models
{
    public class GameSettings
    {
        public double PlayerTurnInterval { set; get; }

        public bool IsPlayerTurnTimerEnabled { set; get; }

        public int PlayersCount { set; get; }

        public int PlayerHandCardsMaxCount { set; get; }

        public int PlayerTableUnitsMaxCount { set; get; }

        public int MaxDeckCardsCount { set; get; }

        public int MaxPlayerMana { set; get; }
    }
}