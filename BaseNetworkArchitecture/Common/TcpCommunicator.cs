using System;
using System.Net;
using System.Net.Sockets;
using BaseNetworkArchitecture.Common.Messages;

namespace BaseNetworkArchitecture.Common
{
    public class TcpCommunicator : INetworkCommunicator
    {
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
                Console.WriteLine(e);
                Client.Close();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                Client.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Connect()
        {
            throw new NotImplementedException();
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
                Console.WriteLine(e);
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

                var lengthBytes = new byte[6];
                Client.GetStream().Read(lengthBytes, 0, 6);

                var msgLength = int.Parse(new NetworkMessage().Encoder.GetString(lengthBytes));
                var clientState = new ClientState(Client, msgLength);

                var result = Client.GetStream().BeginRead(clientState.RcvBuffer, 0, clientState.RcvBuffer.Length,
                    ReadCallback, clientState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Connect(IPAddress IPadress, int port)
        {
            try
            {
                if (Client.Connected)
                    throw new InvalidOperationException("Client is already connected");
                Client.Connect(IPadress, port);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
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
                    recivedNetworkMessage.Content = recivedNetworkMessage.Encoder.GetString(clientState.RcvBuffer);
                    Console.WriteLine("Recieved message from cliet " + recivedNetworkMessage.Content);

                    RunMessageRecievedEvent(new MessageEventArgs
                    {
                        NetworkMessage = recivedNetworkMessage
                    });

                    var lengthBytes = new byte[6];
                    Client.GetStream().Read(lengthBytes, 0, 6);

                    var msgLength = int.Parse(recivedNetworkMessage.Encoder.GetString(lengthBytes));
                    clientState = new ClientState(Client, msgLength);

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
                Console.WriteLine("Client disconnected");
                Client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void WriteCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState) asyncResult.AsyncState;

            try
            {
                clientState.TcpClient.GetStream().EndWrite(asyncResult);

                //Console.WriteLine("Send message to client " + 
                //                  Encoding.UTF8.GetString(clientState.RcvBuffer));

                //clientState = new ClientState(clientState.TcpClient);
                //clientState.TcpClient.GetStream().BeginRead(clientState.RcvBuffer,
                //    clientState.RcvBuffer.Length,
                //    0, ReadCallback, clientState);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Client disconnected");
                Client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}