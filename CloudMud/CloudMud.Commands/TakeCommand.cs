using CloudMud.Core;
using System.Linq;

namespace CloudMud.Commands
{
    public class TakeCommand : Command
    {
        public override string Name => "take";

        private Player _player;

        public TakeCommand(Player player)
        {
            _player = player;
        }

        public override string Execute(string[] args)
        {
            if (_player.CurrentRoom == null)
            {
                return "You are not in a valid room.";
            }

            if (args.Length == 0)
            {
                return "Take what? You need to specify the item to take.";
            }

            string itemName = string.Join(" ", args).ToLower();

            // Use case-insensitive search for the item
            var item = _player.CurrentRoom.Items
                .FirstOrDefault(i => i.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                return $"There is no item called '{itemName}' here.";
            }

            _player.CurrentRoom.Items.Remove(item);
            _player.Inventory.Add(item);
            return $"You picked up the {item.Name}.";
        }

    }
}
