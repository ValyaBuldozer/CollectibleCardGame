using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using BaseNetworkArchitecture.Common.Messages;
using GameData.Enums;
using GameData.Network.Messages;
using Newtonsoft.Json;
using Unity.Attributes;

namespace GameData.Network
{
    public class MessageConverter : IMessageConverter
    {
        public MessageBase DeserializeMessage(NetworkMessage networkMessage)
        {
            if (string.IsNullOrEmpty(networkMessage?.Content))
                throw new NullReferenceException();

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            try
            {
                var deserializedMessage = JsonConvert.DeserializeObject<MessageBase>(
                    networkMessage.Content, settings);

                switch (deserializedMessage.Type)
                {
                    case MessageBaseType.LogInMessage:
                        deserializedMessage.MessageHandler = LogInMessageHandlerBase;
                        break;
                    case MessageBaseType.RegistrationMessage:
                        deserializedMessage.MessageHandler = RegistrationMessageHandlerBase;
                        break;
                    case MessageBaseType.UserInfoRequestMessage:
                        deserializedMessage.MessageHandler = UserInfoRequestMessageHandlerBase;
                        break;
                    case MessageBaseType.SetDeckMesage:
                        deserializedMessage.MessageHandler = SetDeckMessageHandlerBase;
                        break;
                    case MessageBaseType.GameRequestMessage:
                        deserializedMessage.MessageHandler = GameRequestMessageHandlerBase;
                        break;
                    case MessageBaseType.GameStartMessage:
                        deserializedMessage.MessageHandler = GameStartMessageHandlerBase;
                        break;
                    case MessageBaseType.PlayerTurnMessage:
                        deserializedMessage.MessageHandler = PlayerTurnMessageHandlerBase;
                        break;
                    case MessageBaseType.PlayerTurnStartMessage:
                        deserializedMessage.MessageHandler = PlayerTurnStartMessageHandlerBase;
                        break;
                    case MessageBaseType.GameResultMessage:
                        deserializedMessage.MessageHandler = GameResultMessageHandlerBase;
                        break;
                    case MessageBaseType.DisconnectMessage:
                        deserializedMessage.MessageHandler = DisconnectMessageHandlerBase;
                        break;
                    case MessageBaseType.ErrorMessage:
                        deserializedMessage.MessageHandler = ErrorMessageHandlerBase;
                        break;
                    case MessageBaseType.ObserverActionMessage:
                        deserializedMessage.MessageHandler = ObserverActionMessageHandlerBase;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return deserializedMessage;
            }
            catch (JsonSerializationException e)
            {

            }
            catch (JsonReaderException e)
            {
                //if (_concotinationMessage != null)
                //{
                //    _concotinationMessage += networkMessage.Content;
                //    return DeserializeMessage(new NetworkMessage(_concotinationMessage));
                //}

                //_concotinationMessage = networkMessage.Content;
            }
            catch (NullReferenceException e)
            {

            }

            return null;
        }

        public NetworkMessage SerializeMessage(MessageBase messageBase)
        {
            if(messageBase?.Content == null)
                throw new NullReferenceException();

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase,settings));
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

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
            };

            return new NetworkMessage(JsonConvert.SerializeObject(messageBase, settings));
        }




        [Dependency]
        public MessageHandlerBase<LogInMessage> LogInMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<RegistrationMessage> RegistrationMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<GameRequestMessage> GameRequestMessageHandlerBase { set; get; }

        //[Dependency]
        public MessageHandlerBase<DisconnectMessage> DisconnectMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<ErrorMessage> ErrorMessageHandlerBase { set; get; }

        //[Dependency]
        public MessageHandlerBase<GameResultMessage> GameResultMessageHandlerBase { set; get; }

        //[Dependency]
        public MessageHandlerBase<GameStartMessage> GameStartMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<PlayerTurnMessage> PlayerTurnMessageHandlerBase { set; get; }

        //[Dependency]
        public MessageHandlerBase<PlayerTurnStartMessage> PlayerTurnStartMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<SetDeckMessage> SetDeckMessageHandlerBase { set; get; }

        //[Dependency]
        public MessageHandlerBase<UserInfoRequestMessage> UserInfoRequestMessageHandlerBase { set; get; }

        [Dependency]
        public MessageHandlerBase<ObserverActionMessage> ObserverActionMessageHandlerBase { set; get; }

        [Dependency]
        public ILogger Logger { set; get; }
    }
}
