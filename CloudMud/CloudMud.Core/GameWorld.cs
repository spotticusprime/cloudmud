using System.Collections.Generic;

namespace CloudMud.Core
{
    public class GameWorld
    {
        public List<Room> Rooms { get; set; }
        public List<Player> Players { get; set; }

        public GameWorld()
        {
            Rooms = new List<Room>();
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }
    }
}
