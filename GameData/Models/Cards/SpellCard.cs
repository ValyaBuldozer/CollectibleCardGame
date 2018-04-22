using GameData.Enums;

namespace GameData.Models.Cards
{
    public class SpellCard : ICard
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public int Cost { set; get; }

        public bool CanBePlayedOnEnemyTurn { set; get; }

        public int ActionID { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
