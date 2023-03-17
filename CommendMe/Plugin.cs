using System.Reflection;
using System.Linq;
using CommendMe.DataStructure;
using CommendMe.Extension;
using CommendMe.Windows;
using Dalamud.Game.Command;
using Dalamud.Game.ClientState;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using Dalamud.Game.Gui;
using Microsoft.Extensions.DependencyInjection;

namespace CommendMe
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "CommendMe";

        private ServiceProvider _service;

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager,
            [RequiredVersion("1.0")] ChatGui chatGui,
            [RequiredVersion("1.0")] ClientState client)
        {
            // Register configuration
            var config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            config.Initialize(pluginInterface);

            // Command handler
            commandManager.AddHandler("/helloworld", new CommandInfo(HelloWorldCommand)
            {
                HelpMessage = "Hello world!"
            });

            // Register service
            var services = new ServiceCollection()
                .AddSingleton<DalamudPluginInterface>(pluginInterface)
                .AddSingleton<CommandManager>(commandManager)
                .AddSingleton<ChatGui>(chatGui)
                .AddSingleton<ClientState>(client)
                .AddSingleton<Configuration>(config)
                .AddSingleton<WindowSystem>(new WindowSystem("CommendMe"));
            _service = services.BuildServiceProvider();
            foreach (var service in services.ToArray()) _service.GetRequiredService(service.ServiceType);

            // Register all self-build services
            var listServices = Assembly.GetExecutingAssembly().GetAssociatedNamespace<BaseService>("CommendMe.Services");
            foreach (var listService in listServices)
            {
                var instance = (BaseService)ActivatorUtilities.CreateInstance(_service, listService);
                instance.Execute();
            }

            // Register and draw window
            _service.GetRequiredService<DalamudPluginInterface>().UiBuilder.Draw += DrawUI;
            _service.GetRequiredService<DalamudPluginInterface>().UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        private void HelloWorldCommand(string command, string argString)
        {
            _service.GetRequiredService<ChatGui>().Print("Hello world!");
        }

        public void Dispose()
        {
            _service.GetRequiredService<WindowSystem>().RemoveAllWindows();
            _service.GetRequiredService<CommandManager>().RemoveHandler("/helloworld");
        }

        private void DrawUI() => _service.GetRequiredService<WindowSystem>().Draw();

        public void DrawConfigUI() => _service.GetRequiredService<WindowSystem>().OpenWindow<ConfigWindow>();
    }
}
