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
            //var array = new int[] {120,120,120,120,120,26,27,30,30,30,31,32,33,61,62,82,83,84,85};
            //var array = new int[] { 52,37,52,37,52,37,52,37,52,37,52,37,52,37,53,37,52,37,52,37,52,37,52,37,52,37,37,52 };
            var array = new int[] { 48,48,48,48,82,82,83,83,84,84,85,85,86,86,87,87,89,89,91,91,93,93,95,95,96,96,97,97,99,99,100,100,102,102 };
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
