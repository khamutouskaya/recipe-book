using CommonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Configuration;
using CommonEntities.Enums;
using System.Text.Json;

namespace ServerApp.Transport
{
    public class TCPServerProtocol
    {
        protected IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse
            ("127.0.0.1"),
            int.Parse("54621"));

        protected Socket listenSocket
            = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public TCPServerProtocol()
        {
            Console.WriteLine("Server IP: 127.0.0.1 " +
                "Server port: 54621");
            listenSocket.Bind(endPoint);
            listenSocket.Listen(5);
        }

        public Socket AcceptConnectionRequest()
        {
            return listenSocket.Accept();
        }

        public void CloseConnection()
        {
            listenSocket.Shutdown(SocketShutdown.Both);
            listenSocket.Close();
        }

        protected void SendServiceCommand(ServiceCommands command, Socket destination)
        {
            destination.Send(BitConverter.GetBytes((int)command));
        }

        protected ServiceCommands ReceiveServiceCommand(Socket from)
        {
            byte[] buffer = new byte[4];
            from.Receive(buffer);
            return (ServiceCommands)(BitConverter.ToInt32(buffer, 0));
        }

        public void SendAnswerFromServer(AnswerFromServer answer, Socket desination)
        {
            ReceiveServiceCommand(desination);
            desination.Send(BitConverter.GetBytes((int)answer));
        }

        public Command ReceiveCommand(Socket from)
        {
            byte[] commandBuf = new byte[4];
            SendServiceCommand(ServiceCommands.ReadyToReceive, from);
            from.Receive(commandBuf);
            Command command = (Command)(BitConverter.ToInt32(commandBuf, 0));
            return command;
        }

        public void SendString(string str, Socket destination)
        {
            ReceiveServiceCommand(destination);
            destination.Send(BitConverter.GetBytes(str.Length));
            ReceiveServiceCommand(destination);
            destination.Send(Encoding.Default.GetBytes(str));
        }

        public string ReceiveString(Socket from)
        {
            var sizeBuffer = new byte[4];
            SendServiceCommand(ServiceCommands.ReadyToReceive, from);
            from.Receive(sizeBuffer);
            byte[] loginBuffer = new byte[BitConverter.ToInt32(sizeBuffer, 0)];
            SendServiceCommand(ServiceCommands.ReadyToReceive, from);
            from.Receive(loginBuffer);
            return Encoding.Default.GetString(loginBuffer);
        }

        public void SendObject<T>(T obj, Socket destination) where T : class
        {
            SendString(JsonSerializer.Serialize(obj), destination);
        }

        public T ReceiveObject<T>(Socket from) where T : class
        {
            string str = ReceiveString(from);
            return JsonSerializer.Deserialize<T>(str);
        }

        public void SendCollection<T>(List<T> collection, Socket destination)
        {
            SendString(JsonSerializer.Serialize(collection), destination);
        }

        public List<T> ReceiveCollection<T>(Socket from)
        {
            string str = ReceiveString(from);
            return JsonSerializer.Deserialize<List<T>>(str);
        }
    }
}
