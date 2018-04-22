using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Models;

namespace GameData.Controllers.Table
{
    public interface IDealCardsDispatcher
    {
        void DealCardsToPlayer(string username, int count);
        void DealCardsToPlayer(Player player, int count);
    }

    public class DealCardsDispatcher
    {
        private readonly TableCondition _tableCondition;
        private readonly IDeckController _deckController;

        public DealCardsDispatcher(TableCondition tableCondition, IDeckController deckController)
        {
            _tableCondition = tableCondition;
            _deckController = deckController;
        }

        public void DealCardsToPlayer(string username,int count)
        {
            DealCardsToPlayer(_tableCondition.GetPlayerByUsername(username),count);
        }

        public void DealCardsToPlayer(Player player,int count)
        {
            var cards = _deckController.PopCards(player.Username, count);

            foreach (var iCard in cards)
            {
                if(player.HandCards.Count<10)
                    player.HandCards.Add(iCard);
                else
                //todo : card burn event
                    break;
            }
        }


    }
}
