using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Common
{
    public class TcpCommunicator : INetworkCommunicator
    {
        public ILogger Logger { set; get; }

        public bool IsConnected => Client.Connected;

        public TcpCommunicator(TcpClient client)
        {
            Client = client;
        }

        public TcpClient Client { set; get; }

        public bool SendMessage(NetworkMessage networkMessage)
        {
            if (string.IsNullOrEmpty(networkMessage.Content))
                throw new NullReferenceException("Message string was empty");

            try
            {
                var msgBytes = networkMessage.Encoder.GetBytes(networkMessage.Content);
                var lengthBytes = new byte[6];
                var size = networkMessage.Encoder.GetBytes(msgBytes.Length.ToString());
                var length = networkMessage.Encoder.GetString(size);
                size.CopyTo(lengthBytes, 0);

                if (lengthBytes.Length > 6)
                    throw new Exception("Error: message length must be lower than 6");

                //пишем длинну
                Client.GetStream().Write(lengthBytes, 0, lengthBytes.Length);
                Client.GetStream().Write(msgBytes, 0, msgBytes.Length);
                return true;
            }
            catch (SocketException e)
            {
                Logger?.Log(e);
                Client.Close();
                return false;
            }
            catch (Exception e)
            {
                Logger?.Log(e);
                return false;
            }
        }

        public NetworkMessage ReadMessage()
        {
            try
            {
                var retNetworkMessage = new NetworkMessage();

                var lengthBytes = new byte[6];

                var bytesRead = Client.GetStream().Read(lengthBytes, 0, lengthBytes.Length);

                if (int.TryParse(retNetworkMessage.Encoder.GetString(lengthBytes), out var length))
                {
                    var dataBuffer = new byte[length];
                    Client.GetStream().Read(dataBuffer, 0, dataBuffer.Length);
                    retNetworkMessage.Content = retNetworkMessage.Encoder.GetString(dataBuffer);

                    return retNetworkMessage;
                }

                throw new FormatException("Wrong length format");
            }
            catch (SocketException e)
            {
                Logger?.Log(e);
                Client.Close();
                return null;
            }
            catch (Exception e)
            {
                Logger?.Log(e);
                return null;
            }
        }

        public bool Connect()
        {
            try
            {
                //todo : доделать подключение
                return Client.Connected;
            }
            catch (Exception e)
            {
                Logger?.Log(e);
                return false;
            }
        }

        public bool Connect(IPAddress ipAddress, int port)
        {
            if(Client.Connected)
                throw new InvalidOperationException("Connection is already exist");
            try
            {
                if(Client == null)
                    Client=new TcpClient();

                Client.Connect(ipAddress, port);
                return true;
            }
            catch (Exception e)
            {
                Logger?.Log(e);
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                Client.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger?.Log(e);
                return false;
            }
        }

        public event EventHandler<MessageEventArgs> MessageRecievedEvent;

        private void RunMessageRecievedEvent(MessageEventArgs e)
        {
            MessageRecievedEvent?.Invoke(this, e);
        }

        public event EventHandler<BreakConnectionEventArgs> BreakConnectionEvent;

        private void RunBreakConnection(BreakConnectionEventArgs e)
        {
            BreakConnectionEvent?.Invoke(this,e);
        }

        public void StartReadMessages()
        {
            try
            {
                //MessageRecievedEvent += OnMessageRecieved;
                if (Client == null)
                    throw new NullReferenceException();

                //var lengthBytes = new byte[6];
                //Client.GetStream().Read(lengthBytes, 0, 6);

                //var msgLength = int.Parse(new NetworkMessage().Encoder.GetString(lengthBytes));
                var clientState = new ClientState(Client, 6);

                var result = Client.GetStream().BeginRead(clientState.RcvBuffer, 0, clientState.RcvBuffer.Length,
                    ReadCallback, clientState);
            }
            catch (SocketException ex)
            {
                Logger?.Log(ex);
                RunBreakConnection(new BreakConnectionEventArgs()
                {
                    DisconnectReason = ex.ErrorCode.ToString()
                });
            }
            catch (Exception ex)
            {
                Logger?.Log(ex.Message);
            }
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState) asyncResult.AsyncState;

            try
            {
                var recivedNetworkMessage = new NetworkMessage();
                var msgSize = clientState.TcpClient.GetStream().EndRead(asyncResult);

                if (msgSize > 0)
                {
                    int messageLength =
                        int.Parse(recivedNetworkMessage.Encoder.GetString(clientState.RcvBuffer));
                    byte[] messageBuffer = new byte[messageLength];
                    //Logger?.Log("Recieved message from cliet " + recivedNetworkMessage.Content);

                    Client.GetStream().Read(messageBuffer, 0, messageBuffer.Length);
                    recivedNetworkMessage.Content = recivedNetworkMessage.Encoder.GetString(messageBuffer);

                    RunMessageRecievedEvent(new MessageEventArgs
                    {
                        NetworkMessage = recivedNetworkMessage
                    });

                    //var lengthBytes = new byte[6];

                    //var msgLength = int.Parse(recivedNetworkMessage.Encoder.GetString(lengthBytes));
                    clientState = new ClientState(Client, 6);

                    var result = Client.GetStream().BeginRead(clientState.RcvBuffer, 0, clientState.RcvBuffer.Length,
                        ReadCallback, clientState);
                }
                else
                {
                    clientState.TcpClient.Close();
                }
            }
            catch (SocketException e)
            {
                Logger?.LogAndPrint("Client disconnected");
                Client.Close();
            }
            //catch (Exception e)
            //{
            //    Logger?.Log(e);
            //}
            catch (FormatException e)
            {
                Logger?.LogAndPrint("Message read error");
                StartReadMessages();
            }
            catch (IOException e)
            {
                Logger?.LogAndPrint("Соединение разорвано");
                Logger?.Log(e.Message);
                BreakConnectionEvent?.Invoke(this,new BreakConnectionEventArgs());
            }
        }

        private void WriteCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState) asyncResult.AsyncState;

            try
            {
                clientState.TcpClient.GetStream().EndWrite(asyncResult);
            }
            catch (SocketException e)
            {
                Logger?.LogAndPrint("Client disconnected");
                Client.Close();
            }
            catch (Exception e)
            {
                Logger?.Log(e);
            }
        }
    }
}