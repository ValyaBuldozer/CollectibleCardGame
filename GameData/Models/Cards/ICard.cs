namespace GameData.Models.Cards
{
    public abstract class Card
    {
        int ID { set; get; }
        string Name { set; get; }
        string Description { set; get; }
        string BackgroundImagePath { set; get; }
        int Cost { set; get; }
        bool CanBePlayedOnEnemyTurn { set; get; }
    }
}
