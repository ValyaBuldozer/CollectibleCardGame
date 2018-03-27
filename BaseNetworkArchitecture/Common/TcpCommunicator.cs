using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetworkArchitecture.Common
{
    public class TcpCommunicator : INetworkCommunicator
    {
        public TcpClient Client { set; get; }

        public event EventHandler<MessageEventArgs> MessageRecievedEvent;

        public void RunMessageRecievedEvent(MessageEventArgs e)
        {
            MessageRecievedEvent?.Invoke(this, e);
        }

        public bool SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(message.Content))
                throw new NullReferenceException("Message string was empty");

            try
            {
                byte[] msgBytes = message.Encoder.GetBytes(message.Content);
                byte[] lengthBytes = new byte[6];
                byte[] size = message.Encoder.GetBytes(msgBytes.Length.ToString());
                string length = message.Encoder.GetString(size);
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

        public Message ReadMessage()
        {
            try
            {
                Message retMessage = new Message();

                byte[] lengthBytes = new byte[6];

                int bytesRead = Client.GetStream().Read(lengthBytes, 0, lengthBytes.Length);

                if (int.TryParse(retMessage.Encoder.GetString(lengthBytes), out int length))
                {
                    byte[] dataBuffer = new byte[length];
                    Client.GetStream().Read(dataBuffer, 0, dataBuffer.Length);
                    retMessage.Content = retMessage.Encoder.GetString(dataBuffer);

                    return retMessage;
                }
                else
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

        public void StartReadMessages()
        {
            try
            {
                MessageRecievedEvent += OnMessageRecieved;
                if (Client == null)
                    throw new NullReferenceException();

                byte[] lengthBytes = new byte[6];
                Client.GetStream().Read(lengthBytes, 0, 6);

                int msgLength = int.Parse(new Message().Encoder.GetString(lengthBytes));
                var clientState = new ClientState(Client, msgLength);

                var result = Client.GetStream().BeginRead(clientState.RcvBuffer, 0, clientState.RcvBuffer.Length,
                    ReadCallback, clientState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool Connect(IPAddress IPadress, int port)
        {
            try
            {
                Client.Connect(IPadress, port);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                return false;
            }
        }

        public TcpCommunicator(TcpClient client)
        {
            Client = client;
        }

        public void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            string answer = "Server answer " + e.Message.Content;

            byte[] bytes = e.Message.Encoder.GetBytes(answer);
            var clientState = new ClientState(Client, bytes.Length);

            byte[] lengthBytes = new byte[6];
            e.Message.Encoder.GetBytes(bytes.Length.ToString()).CopyTo(lengthBytes, 0);

            clientState.TcpClient.GetStream().Write(lengthBytes, 0, lengthBytes.Length);

            clientState.TcpClient.GetStream().BeginWrite(bytes,
                0, bytes.Length,
                WriteCallback, clientState);
        }

        public void ReadCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState)asyncResult.AsyncState;

            try
            {
                Message recivedMessage = new Message();
                var msgSize = clientState.TcpClient.GetStream().EndRead(asyncResult);

                if (msgSize > 0)
                {
                    recivedMessage.Content = recivedMessage.Encoder.GetString(clientState.RcvBuffer);
                    Console.WriteLine("Recieved message from cliet " + recivedMessage.Content);

                    RunMessageRecievedEvent(new MessageEventArgs()
                    {
                        Message = recivedMessage
                    });

                    byte[] lengthBytes = new byte[6];
                    Client.GetStream().Read(lengthBytes, 0, 6);

                    int msgLength = int.Parse(recivedMessage.Encoder.GetString(lengthBytes));
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

        public void WriteCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState)asyncResult.AsyncState;

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

    public class MessageEventArgs : EventArgs
    {
        public Message Message { set; get; }
        public string Sender { set; get; }
    }
}