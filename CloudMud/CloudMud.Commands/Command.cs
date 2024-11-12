namespace CloudMud.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract string Execute(string[] args);
    }
}
