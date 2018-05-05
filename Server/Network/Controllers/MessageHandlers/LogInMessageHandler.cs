using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Database;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class LogInMessageHandler : MessageHandlerBase<LogInMessage>
    {
        public override IContent Execute(IContent content,object sender = null)
        {
            if(!(content is LogInMessage))
                throw new InvalidOperationException();

            var message = (LogInMessage) content;

            try
            {
                string user = UnityKernel.Get<UserService>().LogIn(message.Username, message.Password);
                message.AnswerData = user;
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
