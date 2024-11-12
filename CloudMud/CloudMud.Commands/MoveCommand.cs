using CloudMud.Core;

namespace CloudMud.Commands
{
    public class MoveCommand : Command
    {
        public override string Name => "move";

        private Player _player;

        public MoveCommand(Player player)
        {
            _player = player;
        }

        public override string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                return "Move where? You must specify a direction.";
            }

            string direction = args[0].ToLower();

            if (_player.CurrentRoom.Exits.ContainsKey(direction))
            {
                _player.CurrentRoom = _player.CurrentRoom.Exits[direction];
                return $"You moved {direction}. You are now in {_player.CurrentRoom.Name}.";
            }
            else
            {
                return "You can't go that way.";
            }
        }
    }
}
