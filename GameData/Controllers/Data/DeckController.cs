using System;
using System.Collections.Generic;
using System.Linq;
using GameData.Models.Cards;
using GameData.Models.Repository;
using Unity.Attributes;

namespace GameData.Controllers.Data
{
    public interface IDeckController
    {
        /// <summary>
        /// Установить колоды в репозиторий
        /// </summary>
        /// <param name="decksDictionary"></param>
        void SetDecks(Dictionary<string, Stack<Card>> decksDictionary);

        /// <summary>
        /// Добавить колоду в репозиторий
        /// </summary>
        /// <param name="username">Имя игрока</param>
        /// <param name="deck">Кололда</param>
        void AddDeck(string username, Stack<Card> deck);

        /// <summary>
        /// Перемешать колоду
        /// </summary>
        /// <param name="username">Имя игрока</param>
        void ShuffleDeck(string username);

        /// <summary>
        /// Перемешать все колоды
        /// </summary>
        void ShuffleDeck();

        /// <summary>
        /// Извлечь верхнюю карту из колоды игрока
        /// </summary>
        /// <param name="username">Ник игрока</param>
        /// <returns>Карта</returns>
        Card PopCard(string username);

        /// <summary>
        /// Извлечь несколько карт из колоды игрока
        /// </summary>
        /// <param name="username">Имя игрока</param>
        /// <param name="count">Количество карт</param>
        /// <returns>Список карт</returns>
        List<Card> PopCards(string username, int count);

        /// <summary>
        /// Посмотреть верхнюю карту колоды
        /// </summary>
        /// <param name="username">Имя игрока</param>
        /// <returns>Карта</returns>
        Card PeekCard(string username);

        /// <summary>
        /// Поместить карту на верх колоды
        /// </summary>
        /// <param name="username">Имя игрока</param>
        /// <param name="card">Карта</param>
        void PushCard(string username, Card card);

        /// <summary>
        /// Получить колоду игрока
        /// </summary>
        /// <param name="username">Имя игрока</param>
        /// <returns>Колода</returns>
        Stack<Card> GetDeck(string username);
    }

    public class DeckController : IDeckController
    {
        [Dependency]
        public DeckRepository Repository { set; get; }

        public void SetDecks(Dictionary<string, Stack<Card>> decksDictionary)
        {
            Repository.Dictionary = decksDictionary;
        }

        public void AddDeck(string username,Stack<Card> deck)
        {
            Repository.Dictionary.Add(username,deck);
        }

        public void ShuffleDeck(string username)
        {
            var stack = GetDeck(username);
            Random rnd = new Random();
            stack = new Stack<Card>(stack.OrderBy(x => rnd.Next()));
            Repository.Dictionary.Remove(username);
            Repository.Dictionary.Add(username,stack);
        }

        public void ShuffleDeck()
        {
            throw new NotImplementedException();
        }


        public Card PopCard(string username)
        {
            var deck = GetDeck(username);

            return deck.Count != 0 ? deck.Pop() : null;
        }

        public List<Card> PopCards(string username, int count)
        {
            var deck = GetDeck(username);
            var retlist = new List<Card>();

            for (int i = 0; i <= count; i++)
            {
                if(deck.Count != 0)
                    retlist.Add(deck.Pop());
                else
                    break;
            }

            return retlist;
        }

        public Card PeekCard(string username)
        {
            return GetDeck(username).Peek();
        }

        public void PushCard(string username, Card card)
        {
            GetDeck(username).Push(card);
        }

        public Stack<Card> GetDeck(string username)
        { 
            if (Repository.Dictionary.TryGetValue(username, out var stack))
                return stack;
            else
                throw new InvalidOperationException();
        }
    }
}
