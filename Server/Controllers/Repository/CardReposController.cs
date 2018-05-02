using System;
using System.Collections.Generic;
using System.Linq;
using GameData.Models.Cards;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers.Repository
{
    public class CardReposController
    {
        private readonly CardRepository _repository;

        public IEnumerable<Card> GetEnumerable => _repository.Collection;

        public CardReposController(CardRepository repository)
        {
            _repository = repository;
        }

        public void Add(Card value)
        {
            _repository.Collection.Add(value);
            _repository.Update();
        }

        public void Remove(Card value)
        {
            _repository.Collection.Remove(value);
            _repository.Update();
        }

        public void Edit(Card value)
        {
            var rValue = _repository.Collection.FirstOrDefault(c => c.ID == value.ID);

            if(rValue == null)
                throw new NullReferenceException("No value was found");

            _repository.Collection.Remove(rValue);
            _repository.Collection.Add(value);

            _repository.Update();
        }

        public Stack<Card> GetDeckById(IEnumerable<int> idCollection)
        {
            if (idCollection == null)
                return null;

            Stack<Card> deck = new Stack<Card>();

            foreach (var id in idCollection)
            {
                Card card = _repository.Collection.FirstOrDefault(c => c.ID == id);
                if(card !=null)
                    deck.Push(card);
            }

            return deck;
        }
    }
}
