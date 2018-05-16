using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network.Messages;
using Unity.Interception.Utilities;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameController
    {
        private readonly INetworkController _networkController;
        private readonly IDataRepositoryController<Card> _cardRepositoryController;

        public GameController(INetworkController networkController,
            GoGameFramePageViewModel goGameFramePageViewModel,GameEngineViewModel gameEngineViewModel,
            IDataRepositoryController<Card> cardRepositoryController)
        {
            _networkController = networkController;
            goGameFramePageViewModel.GameRequest += GameRequestEventHandler;
            gameEngineViewModel.PlayerTurnEvent += ViewModelPlayerTurnEventHandler;
            _cardRepositoryController = cardRepositoryController;
        }

        private void ViewModelPlayerTurnEventHandler(object sender, Services.PlayerTurnRequestEventArgs e)
        {
            if(e.PlayerTurn == null)
                return;

            MessageBase message = new MessageBase(MessageBaseType.PlayerTurnMessage,
                new PlayerTurnMessage(){PlayerTurn = e.PlayerTurn});
            _networkController.SendMessage(message);
        }

        private void GameRequestEventHandler(object sender, Services.GameRequestEventArgs e)
        {
            Random rnd = new Random();
            var array = new int[] {20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44};
            for (int i = 0; i < 24; i++)
            {
                int randomItem = rnd.Next(0, 24);
                int item = array[randomItem];
                array[randomItem] = array[i];
                array[i] = item;
            }

            var deck = new List<int>(array);

            var card = (UnitCard)_cardRepositoryController.GetById(44);
            SendGameRequest(deck,card);
        }

        public void SendGameRequest(List<int> deck, UnitCard card)
        {
            if(deck == null || card == null)
                throw new NullReferenceException();

            MessageBase message = new MessageBase(MessageBaseType.GameRequestMessage,
                new GameRequestMessage()
                {
                    CardDeckIdList = deck,
                    HeroUnitCard = card
                });
            _networkController.SendMessage(message);
        }

        public void PrepareToGame()
        {

        }
    }
}
