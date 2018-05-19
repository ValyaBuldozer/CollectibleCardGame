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
            //var array = new int[] { 21, 22, 23, 24, 25, 26, 27, 30, 30, 30, 31, 32, 33, 60, 61, 62, 63, 64, 82, 83, 84, 85 };
            var array = new int[] { 61,61,61,61,61,61,61,61,61,22, 22, 22, 22, 22, 22, 22, 119, 119, 119, 119, 119, 120, 120, 120, 120, 22, 77, 77, 77, 77, 85,29,29,29,29,29,29,29,29,29,29,29,122,122,122,129,129,129,129,117,118, 117, 118, 117, 118, 117, 118 };
            for (int i = 0; i < array.Length; i++)
            {
                int randomItem = rnd.Next(0, array.Length);
                int item = array[randomItem];
                array[randomItem] = array[i];
                array[i] = item;
            }

            var deck = new List<int>(array);

            var card = (UnitCard)_cardRepositoryController.GetById(1000);
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
