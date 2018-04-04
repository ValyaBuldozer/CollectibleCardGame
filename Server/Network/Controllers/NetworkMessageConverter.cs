using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Enums;
using GameData.Network.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Network.Controllers.MessageHandlers;
using Server.Unity;

namespace Server.Network.Controllers
{
    public class NetworkMessageConverter
    {
        public MessageBase DeserializeMessage(NetworkMessage networkMessage)
        {
            if(string.IsNullOrEmpty(networkMessage?.Content))
                throw new NullReferenceException();

            MessageBase resultMessage;

            try
            {
                var deserializedObj = JsonConvert.DeserializeObject<MessageBase>(networkMessage.Content);

                switch (deserializedObj.Type)
                {
                    case MessageBaseType.LogInMessage:
                        return  new MessageBase(type: MessageBaseType.LogInMessage,
                            content: (((JObject)deserializedObj.Content).ToObject<LogInMessage>()),
                            messageHandler: UnityKernel.Get<LogInMessageHandler>());

                    case MessageBaseType.RegistrationMessage:
                        return new MessageBase(type: MessageBaseType.RegistrationMessage,
                            content: (((JObject)deserializedObj.Content).ToObject<RegistrationMessage>()),
                            messageHandler: UnityKernel.Get<RegistrationMessageHandler>());

                    case MessageBaseType.UserInfoRequestMessage:
                        break;
                    case MessageBaseType.SetDeckMesage:
                        break;
                    case MessageBaseType.GameRequestMessage:
                        return new MessageBase(type: MessageBaseType.GameRequestMessage,
                            content: (((JObject)deserializedObj.Content).ToObject<GameRequestMessage>()),
                            messageHandler: UnityKernel.Get<GameRequestMessageHandler>());

                    case MessageBaseType.GameStartMessage:
                        break;
                    case MessageBaseType.PlayerTurnMessage:
                        break;
                    case MessageBaseType.PlayerTurnStartMessage:
                        break;
                    case MessageBaseType.GameResultMessage:
                        break;
                    case MessageBaseType.DisconnectMessage:
                        break;
                    case MessageBaseType.ErrorMessage:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch(JsonSerializationException) { }
            catch(NullReferenceException) { }

            return null;
        }

        public NetworkMessage SerializeMessage(MessageBase messageBase)
        {
            if(messageBase?.Content == null)
                throw new NullReferenceException();

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase));
        }

        public NetworkMessage SerializeMessage(IContent messageBase)
        {
            if (messageBase == null)
                throw new NullReferenceException();

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase));
        }
    }
}
