using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using Newtonsoft.Json;
using Unity.Attributes;

namespace GameData.Models.Repository
{
    public class CardRepository
    {
        public List<Card> Collection { private set; get; }

        public CardRepository()
        {
            Collection = new List<Card>();
        }

        [InjectionConstructor]
        public CardRepository(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            Collection = JsonConvert.DeserializeObject<List<Card>>(json, settings);
        }

        public CardRepository(IEnumerable<Card> collection)
        {
            Collection = new List<Card>(collection);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
