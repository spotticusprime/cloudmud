using CloudMud.Core;

namespace CloudMud.Commands
{
    public class LookCommand : Command
    {
        public override string Name => "look";

        private Player _player;

        public LookCommand(Player player)
        {
            _player = player;
        }

        public override string Execute(string[] args)
        {
            if (_player.CurrentRoom == null)
            {
                return "You are nowhere.";
            }

            string description = $"{_player.CurrentRoom.Name}\n{_player.CurrentRoom.Description}\n";
            if (_player.CurrentRoom.Items.Count > 0)
            {
                description += "Items here: ";
                foreach (var item in _player.CurrentRoom.Items)
                {
                    description += $"{item.Name}, ";
                }
            }

            return description.TrimEnd(',', ' ');
        }
    }
}
