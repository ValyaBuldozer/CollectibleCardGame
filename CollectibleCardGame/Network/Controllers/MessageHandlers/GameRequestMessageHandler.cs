using System;
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Network;
using GameData.Network.Messages;
using Unity.Attributes;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : MessageHandlerBase<GameRequestMessage>
    {
        private readonly GoGameFramePageViewModel _goGameViewModel;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public GameRequestMessageHandler(GoGameFramePageViewModel goGameFramePage,
           MainWindowViewModel mainWindowViewModel)
        {
            _goGameViewModel = goGameFramePage;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (content is GameRequestMessage message)
            {
                if((bool)message.AnswerData)
                {
                    _goGameViewModel.StopBusyIndicator();
                    _mainWindowViewModel.SetGameEngineFrame();
                }
                else
                    _goGameViewModel.StartBusyIndicator("Поиск игры");
            }

            return null;
        }
    }
}
