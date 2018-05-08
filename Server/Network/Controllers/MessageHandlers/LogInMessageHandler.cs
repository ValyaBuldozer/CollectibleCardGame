using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Database;
using Server.Network.Models;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class LogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        public override IContent Execute(IContent content,object sender)
        {
            if(!(content is LogInMessage))
                throw new InvalidOperationException();

            var message = (LogInMessage) content;
            var client = (Client) sender;

            try
            {
                var user = UnityKernel.Get<UserService>().LogIn(message.Username, message.Password);
                if (user != null)
                {
                    message.AnswerData = user.Username;
                    client.User = user;
                    client.ClientConnection.IdentificatorTocken = user.Username;
                }

                return message;
            }
            catch (Exception e)
            {
                //todo : запилить тут перехват пользовательского исключения - должна передаваться ошибка
                return new ErrorMessage();
            }
        }
    }
}
