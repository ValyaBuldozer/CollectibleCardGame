namespace GameData.Models.Cards
{
    public interface ICard
    {
        int ID { set; get; }
        string Name { set; get; }
        string Description { set; get; }
        int Cost { set; get; }
        bool CanBePlayedOnEnemyTurn { set; get; }
    }
}
