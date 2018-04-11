using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using CollectibleCardGame.Network.Controllers.MessageHandlers;
using CollectibleCardGame.Unity;
using GameData.Enums;
using GameData.Network.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CollectibleCardGame.Network.Controllers
{
    public class NetworkMessageConverter
    {
        public MessageBase DeserializeMessage(NetworkMessage networkMessage)
        {
            if (string.IsNullOrEmpty(networkMessage?.Content))
                throw new NullReferenceException();

            try
            {
                var deserializedObj = JsonConvert.DeserializeObject<MessageBase>(networkMessage.Content);

                switch (deserializedObj.Type)
                {
                    case MessageBaseType.LogInMessage:
                        return new MessageBase(type: MessageBaseType.LogInMessage,
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
            catch (JsonSerializationException e)
            {
                UnityKernel.Get<ILogger>().Log(e);
            }
            catch (NullReferenceException e) { }

            return null;
        }

        public NetworkMessage SerializeMessage(MessageBase messageBase)
        {
            if (messageBase?.Content == null)
                throw new NullReferenceException();

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase));
        }

        public NetworkMessage SerializeMessage(IContent content)
        {
            if (content == null)
                throw new NullReferenceException();

            MessageBase messageBase;

            switch (content.GetType().Name)
            {
                case nameof(LogInMessage):
                    messageBase = new MessageBase(MessageBaseType.LogInMessage, content);
                    break;
                case nameof(RegistrationMessage):
                    messageBase = new MessageBase(MessageBaseType.RegistrationMessage, content);
                    break;
                case nameof(DisconnectMessage):
                    messageBase = new MessageBase(MessageBaseType.DisconnectMessage, content);
                    break;
                case nameof(ErrorMessage):
                    messageBase = new MessageBase(MessageBaseType.ErrorMessage, content);
                    break;
                case nameof(GameRequestMessage):
                    messageBase = new MessageBase(MessageBaseType.GameRequestMessage, content);
                    break;
                case nameof(GameStartMessage):
                    messageBase = new MessageBase(MessageBaseType.GameStartMessage, content);
                    break;
                case nameof(PlayerTurnMessage):
                    messageBase = new MessageBase(MessageBaseType.PlayerTurnMessage, content);
                    break;
                case nameof(PlayerTurnStartMessage):
                    messageBase = new MessageBase(MessageBaseType.PlayerTurnStartMessage, content);
                    break;
                case nameof(SetDeckMessage):
                    messageBase = new MessageBase(MessageBaseType.SetDeckMesage, content);
                    break;
                case nameof(UserInfoRequestMessage):
                    messageBase = new MessageBase(MessageBaseType.UserInfoRequestMessage, content);
                    break;
                default:
                    messageBase = null;
                    break;
            }

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase));
        }
    }
}
