using System.Collections.Generic;

namespace CloudMud.Commands
{
    public class CommandProcessor
    {
        private Dictionary<string, Command> _commands;

        public CommandProcessor()
        {
            _commands = new Dictionary<string, Command>();
        }

        public void RegisterCommand(Command command)
        {
            _commands[command.Name] = command;
        }

        public string ProcessCommand(string input)
        {
            var parts = input.Split(' ');
            var commandName = parts[0].ToLower();
            var args = parts.Length > 1 ? parts[1..] : new string[] { };

            if (_commands.ContainsKey(commandName))
            {
                return _commands[commandName].Execute(args);
            }

            return "Unknown command.";
        }
    }
}
