using CloudMud.Network;
using System;

namespace CloudMud.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer(8080);
            server.Start();

            // Keep the server running by waiting indefinitely.
            Console.WriteLine("Press Ctrl + C to stop the server...");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
