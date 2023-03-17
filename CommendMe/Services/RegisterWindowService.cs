using System;
using System.Reflection;
using CommendMe.DataStructure;
using CommendMe.Extension;
using Dalamud.Interface.Windowing;
using Microsoft.Extensions.DependencyInjection;

namespace CommendMe.Services
{
    public class RegisterWindowService : BaseService
    {
        private WindowSystem _window;
        private IServiceProvider _service;

        public RegisterWindowService(IServiceProvider service, WindowSystem windowSystem, CommandList cmdList)
        {
            _window = windowSystem;
            _service = service;
        }

        public override void Execute()
        {
            var listWindow = Assembly.GetExecutingAssembly().GetAssociatedNamespace<Window>("CommendMe.Windows");
            
            foreach (var window in listWindow)
            {
                var instance = (Window)ActivatorUtilities.CreateInstance(_service, window);
                _window.AddWindow(instance);
            }
        }
    }
}