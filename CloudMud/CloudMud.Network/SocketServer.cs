using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CloudMud.Core;
using CloudMud.Commands;
using System.Collections.Generic;

namespace CloudMud.Network
{
    public class SocketServer
    {
        private TcpListener _server;
        private bool _isRunning;
        private GameWorld _gameWorld;

        public SocketServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _gameWorld = new GameWorld(); // Initialize GameWorld

            // Load rooms from JSON files
            var room1 = Room.LoadFromFile("Scripts/Rooms/Room1.json");
            var room2 = Room.LoadFromFile("Scripts/Rooms/Room2.json");

            // Connect rooms based on exits
            room1.Exits["north"] = room2; // Use the actual room object
            room2.Exits["south"] = room1;


            _gameWorld.AddRoom(room1);
            _gameWorld.AddRoom(room2);
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
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };

            Player player = new Player($"Player_{Guid.NewGuid()}");
            player.CurrentRoom = _gameWorld.Rooms[0]; // Start in the first room
            _gameWorld.AddPlayer(player);

            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.RegisterCommand(new LookCommand(player));
            commandProcessor.RegisterCommand(new MoveCommand(player));
            commandProcessor.RegisterCommand(new TakeCommand(player));
            commandProcessor.RegisterCommand(new DropCommand(player));

            Console.WriteLine($"New player connected: {player.Name}");

            try
            {
                while (_isRunning)
                {
                    string? message = await reader.ReadLineAsync();
                    if (message == null)
                    {
                        break;
                    }

                    Console.WriteLine($"Received from {player.Name}: {message}");

                    // Process command using the command processor.
                    string response = commandProcessor.ProcessCommand(message.Trim());
                    await writer.WriteLineAsync(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                _gameWorld.Players.Remove(player);
                client.Close();
                Console.WriteLine($"Player disconnected: {player.Name}");
            }
        }
    }
}
