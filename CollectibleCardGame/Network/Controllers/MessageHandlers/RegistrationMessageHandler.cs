using System.Windows.Threading;
using GameData.Network;
using GameData.Network.Messages;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.Network.Controllers.MessageHandlers
{
    public class RegistrationMessageHandler : MessageHandlerBase<RegistrationMessage>
    {
        private readonly Dispatcher _currentDispatcher;

        public RegistrationMessageHandler()
        {
            _currentDispatcher = Dispatcher.CurrentDispatcher;
        }

        public override IContent Execute(IContent content, object sender)
        {
            if (!(content is RegistrationMessage message)) return null;

            _currentDispatcher.Invoke(() => MessageBox.Show("Пользователь зарегистрирован"));
            return null;
        }
    }
}