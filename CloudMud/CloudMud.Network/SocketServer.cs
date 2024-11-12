using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudMud.Network
{
    public class SocketServer
    {
        private TcpListener _server;
        private bool _isRunning;

        public SocketServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _server.Start();
            _isRunning = true;
            Console.WriteLine("Server started...");
            ListenForClients();
        }

        private async void ListenForClients()
        {
            while (_isRunning)
            {
                var client = await _server.AcceptTcpClientAsync();
                Console.WriteLine("New player connected");
                HandleClient(client);
            }
        }

        private async void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {message}");
                byte[] response = Encoding.ASCII.GetBytes("Message received\n");
                await stream.WriteAsync(response, 0, response.Length);
            }

            client.Close();
            Console.WriteLine("Player disconnected");
        }
    }
}
