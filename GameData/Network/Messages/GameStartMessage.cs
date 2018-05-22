namespace GameData.Network.Messages
{
    public class GameStartMessage : IContent
    {
        //public TableCondition TableCondition { set; get; }
        public string EnemyUsername { set; get; }
        public object AnswerData { set; get; }
    }
}