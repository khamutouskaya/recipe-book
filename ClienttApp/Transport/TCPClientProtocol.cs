using CommonEntities.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClienttApp.Transport
{
    public class TCPClientProtocol
    {
        protected IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse("54621"));

        protected Socket connectionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        protected void SendServiceCommand(ServiceCommands command)
        {
            connectionSocket.Send(BitConverter.GetBytes((int)command));
        }

        protected ServiceCommands ReceiveServiceCommand()
        {
            byte[] buffer = new byte[4];
            connectionSocket.Receive(buffer);
            return (ServiceCommands)(BitConverter.ToInt32(buffer, 0));
        }

        public bool Connect()
        {
            connectionSocket.Connect(serverEndPoint);
            return connectionSocket.Connected;
        }

        public void SendCommand(Command command)
        {
            ReceiveServiceCommand();
            connectionSocket.Send(BitConverter.GetBytes((int)command));
        }

        public void Disconnect()
        {
            SendCommand(Command.Disconnect);
        } 

        public AnswerFromServer ReceiveAnswerFromServer()
        {
            byte[] answerBuf = new byte[4];
            SendServiceCommand(ServiceCommands.ReadyToReceive);
            connectionSocket.Receive(answerBuf);
            AnswerFromServer answer = (AnswerFromServer)(BitConverter.ToInt32(answerBuf, 0));
            return answer;
        }

        public string ReceiveString()
        {
            var sizeBuffer = new byte[4];
            SendServiceCommand(ServiceCommands.ReadyToReceive);
            connectionSocket.Receive(sizeBuffer);
            byte[] loginBuffer = new byte[BitConverter.ToInt32(sizeBuffer, 0)];
            SendServiceCommand(ServiceCommands.ReadyToReceive);
            connectionSocket.Receive(loginBuffer);
            return Encoding.Default.GetString(loginBuffer);
        }

        public void SendString(string str)
        {
            ReceiveServiceCommand();
            connectionSocket.Send(BitConverter.GetBytes(str.Length));
            ReceiveServiceCommand();
            connectionSocket.Send(Encoding.Default.GetBytes(str));
        }

        public T ReceiveObject<T>() where T : class
        {
            string str = ReceiveString();
            return JsonSerializer.Deserialize<T>(str);
        }

        public void SendObject<T>(T obj) where T : class
        {
            SendString(JsonSerializer.Serialize(obj));
        }

        public List<T> ReceiveCollection<T>()
        {
            string str = ReceiveString();
            return JsonSerializer.Deserialize<List<T>>(str);
        }

        public void SendCollection<T>(List<T> collection)
        {
            SendString(JsonSerializer.Serialize(collection));
        }
    }
}
