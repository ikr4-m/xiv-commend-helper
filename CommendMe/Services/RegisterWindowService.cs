using System;
using System.Reflection;
using System.Linq;
using CommendMe.DataStructure;
using Dalamud.Interface.Windowing;
using Dalamud.Game.Gui;
using Microsoft.Extensions.DependencyInjection;

namespace CommendMe.Services
{
    public class RegisterWindowService : BaseService
    {
        private WindowSystem _window;
        private ChatGui _chatGui;
        private IServiceProvider _service;

        public RegisterWindowService(IServiceProvider service, WindowSystem windowSystem, ChatGui chatGui)
        {
            _window = windowSystem;
            _chatGui = chatGui;
            _service = service;
        }

        public override void Execute()
        {
            var listWindow = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsClass && x.Namespace == "CommendMe.Windows")
                .Where(x => x.IsSubclassOf(typeof(Window)));
            
            foreach (var window in listWindow)
            {
                var instance = (Window)ActivatorUtilities.CreateInstance(_service, window);
                _window.AddWindow(instance);
            }
        }
    }
}