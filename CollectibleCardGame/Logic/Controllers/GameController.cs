using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network.Messages;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameController
    {
        private readonly INetworkController _networkController;

        public GameController(INetworkController networkController,
            GoGameFramePageViewModel goGameFramePageViewModel,GameEngineViewModel gameEngineViewModel)
        {
            _networkController = networkController;
            goGameFramePageViewModel.GameRequest += GameRequestEventHandler;
            gameEngineViewModel.PlayerTurnEvent += ViewModelPlayerTurnEventHandler;
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
            var deck = new List<int>()
            {
                301,302,303,304,301,302,303,304,301,302,303,304,301,302,303,304
            };
            var card = new UnitCard()
            {
                BaseHP = 30,
                BaseAttack = 0,
                AttackPriority = 1,
                Name = "TestHero"
            };
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
