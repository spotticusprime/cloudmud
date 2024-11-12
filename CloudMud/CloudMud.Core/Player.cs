using System.Collections.Generic;

namespace CloudMud.Core
{
    public class Player
    {
        public string Name { get; set; }
        public Room? CurrentRoom { get; set; }

        public List<Item> Inventory { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
        }
    }
}
