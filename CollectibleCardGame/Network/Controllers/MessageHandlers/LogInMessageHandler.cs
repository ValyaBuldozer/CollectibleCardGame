using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Models;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Network;
using GameData.Network.Messages;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class LogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly CurrentUser _user;

        public LogInMessageHandler(MainWindowViewModel mainWindowViewModel,CurrentUser user)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _user = user;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (content is LogInMessage message && message.AnswerData is string username)
            {
                _user.Username = username;
                _mainWindowViewModel.SetMainMenuFrame();
                _mainWindowViewModel.Title = "Collectible card game - " + username;
            }

            return content;
        }
    }
}
