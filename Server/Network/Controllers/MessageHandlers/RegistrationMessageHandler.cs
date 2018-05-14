using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Network;
using GameData.Network.Messages;
using Server.Controllers;
using Server.Exceptions;
using Server.Unity;

namespace Server.Network.Controllers.MessageHandlers
{
    public class RegistrationMessageHandler : MessageHandlerBase<RegistrationMessage>
    {
        public override IContent Execute(IContent content,object sender = null)
        {
            if(!(content is RegistrationMessage))
                throw new InvalidOperationException();

            var message = (RegistrationMessage) content;

            try
            {
                var user = UnityKernel.Get<UserService>().RegisterUser(message.Username, message.Password);
                message.AnswerData = user;
                return message;
            }
            catch (UserServiceException e)
            {
                return new ErrorMessage(){ErrorInfo = e.Message};
            }
        }
    }
}
