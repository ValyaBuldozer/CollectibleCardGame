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
        private readonly MainWindowViewModel _mainViewModel;

        public GameController(INetworkController networkController,
            GoGameFramePageViewModel goGameFramePageViewModel,GameEngineViewModel gameEngineViewModel,
            IDataRepositoryController<Card> cardRepositoryController,
            MainWindowViewModel mainViewModel)
        {
            _networkController = networkController;
            goGameFramePageViewModel.GameRequest += GameRequestEventHandler;
            gameEngineViewModel.PlayerTurnEvent += ViewModelPlayerTurnEventHandler;
            _cardRepositoryController = cardRepositoryController;
            _mainViewModel = mainViewModel;
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

            //var array = new int[] {120,120,120,120,120,26,27,30,30,30,31,32,33,61,62,82,83,84,85};

            var array = new[]
            {
                142, 142, 142, 143, 143, 142, 142, 143, 143, 142, 142, 143, 143, 142, 142, 143, 143, 142, 143, 142, 143,
                142, 142, 36,37,116,38,39,40,41,42,43,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,136,141
            };

            array.ForEach(c =>
            {
                if (_cardRepositoryController.GetById(c) == null)
                    throw new NullReferenceException();
            });
            for (int i = 0; i < array.Length; i++)
            {
                int randomItem = rnd.Next(0, array.Length);
                int item = array[randomItem];
                array[randomItem] = array[i];
                array[i] = item;
            }

            var deck = new List<int>(array);

            var card = (UnitCard)_cardRepositoryController.GetById(3000);
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

        public void EndGame()
        {
            _mainViewModel.SetMainMenuFrame();
        }
    }
}
