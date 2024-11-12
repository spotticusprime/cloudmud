using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CloudMud.Core
{
    public class Room
    {
        public string Name { get; set; } = string.Empty; // Ensure non-nullable
        public string Description { get; set; } = string.Empty; // Ensure non-nullable
        public Dictionary<string, Room> Exits { get; set; } = new Dictionary<string, Room>();
        public List<Item> Items { get; set; } = new List<Item>();

        public Room()
        {
            // Already initialized properties to prevent null issues
        }

        public static Room LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Room? room = JsonConvert.DeserializeObject<Room>(json);

            if (room == null)
            {
                throw new InvalidDataException($"Failed to load room from file: {filePath}");
            }

            return room;
        }
    }
}
