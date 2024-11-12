using CloudMud.Core;
using System.Text;

namespace CloudMud.Commands
{
    public class InventoryCommand : Command
    {
        public override string Name => "inventory";

        private Player _player;

        public InventoryCommand(Player player)
        {
            _player = player;
        }

        public override string Execute(string[] args)
        {
            if (_player.Inventory.Count == 0)
            {
                return "You are not carrying anything.";
            }

            StringBuilder inventoryDescription = new StringBuilder("You are carrying:\n");
            foreach (var item in _player.Inventory)
            {
                inventoryDescription.AppendLine($"- {item.Name}");
            }

            return inventoryDescription.ToString();
        }
    }
}
