using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class GameRequestMessageHandler : MessageHandlerBase<GameRequestMessage>
    {
        private readonly GoGameFramePageViewModel _goGameViewModel;
        private readonly MessageHandlerBase<LogInMessage> _logInMessageHandler;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public GameRequestMessageHandler(GoGameFramePageViewModel goGameFramePage,
            MainWindowViewModel mainWindowViewModel,
            MessageHandlerBase<LogInMessage> logInMessageHandler)
        {
            _goGameViewModel = goGameFramePage;
            _mainWindowViewModel = mainWindowViewModel;
            _logInMessageHandler = logInMessageHandler;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (content is GameRequestMessage message)
            {
                if (message.AnswerData is LogInMessage logInMessage)
                {
                    _logInMessageHandler.Execute(logInMessage, null);
                    _goGameViewModel.StopBusyIndicator();
                    _mainWindowViewModel.SetGameEngineFrame();
                    return null;
                }

                if ((bool) message.AnswerData)
                {
                    _goGameViewModel.StopBusyIndicator();
                    _mainWindowViewModel.SetGameEngineFrame();
                }
                else
                {
                    _goGameViewModel.StartBusyIndicator("Поиск игры");
                }
            }

            return null;
        }
    }
}