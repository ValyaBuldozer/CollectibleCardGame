using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace GameData.Models.Repository
{
    public class DeckRepository
    {
        public Dictionary<string,Stack<Card>> Dictionary { set; get; }

        public DeckRepository()
        {
            Dictionary = new Dictionary<string, Stack<Card>>();
        }
    }
}
