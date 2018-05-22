using System;
using System.Collections.Generic;
using GameData.Models.Cards;
using Newtonsoft.Json;
using Unity.Attributes;

namespace GameData.Models.Repository
{
    public class CardRepository
    {
        public CardRepository()
        {
            Collection = new List<Card>();
        }

        [InjectionConstructor]
        public CardRepository(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            Collection = JsonConvert.DeserializeObject<List<Card>>(json, settings);
        }

        public CardRepository(IEnumerable<Card> collection)
        {
            Collection = new List<Card>(collection);
        }

        public List<Card> Collection { get; }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}