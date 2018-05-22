using System.Collections.Generic;
using GameData.Models.Cards;

namespace GameData.Models.Repository
{
    public class DeckRepository
    {
        public DeckRepository()
        {
            Dictionary = new Dictionary<string, Stack<Card>>();
        }

        public Dictionary<string, Stack<Card>> Dictionary { set; get; }
    }
}