using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Models;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class LogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly CurrentUserService _userService;
        private readonly DeckSettingsViewModel _deckSettingsViewModel;

        public LogInMessageHandler(MainWindowViewModel mainWindowViewModel,CurrentUserService userService,
            DeckSettingsViewModel deckSettingsViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _userService = userService;
            _deckSettingsViewModel = deckSettingsViewModel;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (content is LogInMessage message && message.AnswerData is UserInfo userInfo)
            {
                _userService.SetData(userInfo);
                _mainWindowViewModel.SetMainMenuFrame();
                _mainWindowViewModel.Title = "Collectible card game - " + userInfo.Username;
                _deckSettingsViewModel.UpdateDecks();
            }

            return content;
        }
    }
}
