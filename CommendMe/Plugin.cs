using Dalamud.Game.Command;
using Dalamud.Game.ClientState;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using Dalamud.Game.Gui;

namespace CommendMe
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "CommendMe";

        public WindowSystem WindowSystem = new("CommendMe");
        public DalamudPluginInterface PluginInterface { get; init; }
        public CommandManager CommandManager { get; init; }
        public ChatGui ChatGui { get; init; }
        public Configuration Configuration { get; init; }
        public ClientState Client { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager,
            [RequiredVersion("1.0")] ChatGui chatGui,
            [RequiredVersion("1.0")] ClientState client)
        {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;
            ChatGui = chatGui;
            Client = client;
            
            // Register configuration
            Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(this.PluginInterface);

            // Draw window
            PluginInterface.UiBuilder.Draw += DrawUI;
            //PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
        }

        private void DrawUI() => WindowSystem.Draw();

        //public void DrawConfigUI() => this.WindowSystem.OpenWindow(typeof(ConfigWindow));
    }
}
