using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using TcpClient = NetCoreServer.TcpClient;

namespace ConsoleApp1
{

    class ChatClient : TcpClient
    {
        public ChatClient(string address, int port) : base(address, port) { }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            
            Console.WriteLine(Encoding.Default.GetString(buffer, (int)offset, (int)size));
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP client caught an error with code {error}");
        }

        private bool _stop;
    }
    class Program
    {
        static void Main(string[] args)
        {
            // TCP server address
            string address = "192.168.1.254";
            if (args.Length > 0)
                address = args[0];

            // TCP server port
            int port = 30003;
            if (args.Length > 1)
                port = int.Parse(args[1]);

            Console.WriteLine($"TCP server address: {address}");
            Console.WriteLine($"TCP server port: {port}");

            Console.WriteLine();

            // Create a new TCP chat client
            var client = new ChatClient(address, port);

            // Connect the client
            Console.Write("Client connecting...");
            client.ConnectAsync();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the client or '!' to reconnect the client...");


            while (true)
            {
                var aaa = "01 03 00 00 00 02 C4 0B";
                var ccc = StringToHexByte(aaa);
                client.SendAsync(ccc);
                Thread.Sleep(1000);
            }
            // Perform text input
            //for (; ; )
            //{
            //    string line = Console.ReadLine();
            //    if (string.IsNullOrEmpty(line))
            //        break;

            //    // Disconnect the client
            //    if (line == "!")
            //    {
            //        Console.Write("Client disconnecting...");
            //        client.DisconnectAsync();
            //        Console.WriteLine("Done!");
            //        continue;
            //    }

            //    // Send the entered text to the chat server
            //    client.SendAsync(line);
            //}

            // Disconnect the client
            Console.Write("Client disconnecting...");
            client.DisconnectAndStop();
            Console.WriteLine("Done!");
            Console.WriteLine("Hello World!");
        }

        public static byte[] StringToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
