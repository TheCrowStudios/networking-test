using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Client
    {
        //public static Client instance;
        public static int dataBufferSize = 4096;

        public static string ip = "127.0.0.1";
        public static int port = 2405;
        public int myId = 0;
        public TCP tcp = new TCP();

        /*public Client()
        {
            if (instance == null)
            {
                instance = this;
            } else if (instance != this)
            {
                Console.WriteLine("A client instance already exists");
            }
        }*/

        public Client(string _ip)
        {
            ip = _ip;
        }

        public class TCP
        {
            public TcpClient socket;

            private NetworkStream stream;
            private byte[] receiveBuffer;

            public void Connect()
            {
                Console.WriteLine($"Connecting to: {ip}:{port}...");
                socket = new TcpClient()
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(ip, port, ConnectCallback, null);
            }

            private void ConnectCallback(IAsyncResult _result)
            {
                socket.EndConnect(_result);

                if (!socket.Connected)
                {
                    return;
                } else
                {
                    Console.WriteLine("Connected");
                }

                //socket.EndConnect(_result);

                stream = socket.GetStream();

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        // Disconnect
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    // Handle data

                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void ConnectToServer()
        {
            tcp.Connect();
        }
    }
}
