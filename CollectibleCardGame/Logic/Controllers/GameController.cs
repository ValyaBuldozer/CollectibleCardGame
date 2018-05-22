using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network.Messages;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameController
    {
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly MainWindowViewModel _mainViewModel;
        private readonly INetworkController _networkController;

        public GameController(INetworkController networkController,
            GoGameFramePageViewModel goGameFramePageViewModel, GameEngineViewModel gameEngineViewModel,
            IDataRepositoryController<Card> cardRepositoryController,
            MainWindowViewModel mainViewModel)
        {
            _networkController = networkController;
            goGameFramePageViewModel.GameRequest += GameRequestEventHandler;
            gameEngineViewModel.PlayerTurnEvent += ViewModelPlayerTurnEventHandler;
            _cardRepositoryController = cardRepositoryController;
            _mainViewModel = mainViewModel;
        }

        private void ViewModelPlayerTurnEventHandler(object sender, PlayerTurnRequestEventArgs e)
        {
            if (e.PlayerTurn == null)
                return;

            var message = new MessageBase(MessageBaseType.PlayerTurnMessage,
                new PlayerTurnMessage {PlayerTurn = e.PlayerTurn});
            _networkController.SendMessage(message);
        }

        private void GameRequestEventHandler(object sender, GameRequestEventArgs e)
        {
            var message = new MessageBase(MessageBaseType.GameRequestMessage,
                new GameRequestMessage
                {
                    Fraction = e.Fraction
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