using System.Collections.Generic;

namespace CommendMe.DataStructure
{
    public abstract class BaseCommand
    {
        public string? Command { get; set; }
        public string[]? CommandLiterate { get; set; }
        public string HelpMessage { get; set; } = "";
        
        public abstract void Execute(string command, string args);
    }

    public class CommandList
    {
        public List<string> List = new();
    }
}