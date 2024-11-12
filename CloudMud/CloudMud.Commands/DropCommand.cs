using CloudMud.Core;
using System.Linq;

namespace CloudMud.Commands
{
    public class DropCommand : Command
    {
        public override string Name => "drop";

        private Player _player;

        public DropCommand(Player player)
        {
            _player = player;
        }

        public override string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                return "Drop what? You need to specify the item to drop.";
            }

            string itemName = string.Join(" ", args).ToLower();

            // Use case-insensitive search for the item
            var item = _player.Inventory
                .FirstOrDefault(i => i.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                return $"You don't have an item called '{itemName}'.";
            }

            _player.Inventory.Remove(item);
            _player.CurrentRoom?.Items.Add(item);
            return $"You dropped the {item.Name}.";
        }
    }
}
