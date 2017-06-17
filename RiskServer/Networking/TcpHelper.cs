using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RiskServer.Networking
{
    class TcpHelper
    {
        private static TcpListener Listener { get; set; }
        private static bool Accept { get; set; }

        public static void StartServer(int port)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            Listener = new TcpListener(address, port);

            Listener.Start();
            Accept = true;

            Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");
            ListenForClients();
        }

        public static async void ListenForClients()
        {
            if (Listener != null && Accept)
            {

                // Continue listening.  
                while (true)
                {
                    Console.WriteLine("Waiting for client...");
                    var client = await Listener.AcceptTcpClientAsync(); // Get the client  
                    Console.WriteLine("Client connected");
                    ProcessClient(client);
                       
                    
                }
            }
        }

        private static void ProcessClient(TcpClient client) {
            using (var networkstream = client.GetStream()) {
                while (true) {
                    var headerBuffer = ReadToCompletion(networkstream,4);

                    //convert to big endian;
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(headerBuffer);

                    //data length to read
                    var length = BitConverter.ToInt32(headerBuffer, 0);
                    var dataBuffer = ReadToCompletion(networkstream, length);

                    var memStream = new MemoryStream(dataBuffer, 0, length);

                    //decompress and process message
                    var msg = DeflateUtils.UnZipStr(memStream);
                    Console.WriteLine("Received: " + msg);
                }
            }
        }

        private static byte[] ReadToCompletion(NetworkStream networkStream, int amount) {
            var bytesRead = 0;
            var headerBuffer = new byte[amount];
            while (bytesRead < amount && networkStream.CanRead) {
                bytesRead += networkStream.Read(headerBuffer, bytesRead, headerBuffer.Length - bytesRead);
            }
            return headerBuffer;
        }

    }
}
