using CommendMe.DataStructure;
using CommendMe.Extension;
using CommendMe.Windows;
using Dalamud.Interface.Windowing;

namespace CommendMe.Command
{
    public class OpenConfigCommand : BaseCommand
    {
        private WindowSystem _window;

        public OpenConfigCommand(WindowSystem window)
        {
            Command = "/cmconfig";
            HelpMessage = "Open CommendMe config.";

            _window = window;
        }

        public override void Execute(string command, string argString)
        {
            _window.OpenWindow<ConfigWindow>();
        }
    }
}