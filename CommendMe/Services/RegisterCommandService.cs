using System;
using System.Reflection;
using CommendMe.DataStructure;
using CommendMe.Extension;
using Dalamud.Game.Command;
using Microsoft.Extensions.DependencyInjection;

namespace CommendMe.Services
{
    public class RegisterCommandService : BaseService
    {
        private IServiceProvider _service;
        private CommandManager _command;
        private CommandList _cmdList;

        public RegisterCommandService(IServiceProvider service, CommandManager cmdManager, CommandList cmdList)
        {
            _service = service;
            _command = cmdManager;
            _cmdList = cmdList;
        }

        public override void Execute()
        {
            var listCommand = Assembly.GetExecutingAssembly().GetAssociatedNamespace<BaseCommand>("CommendMe.Command");

            foreach (var command in listCommand)
            {
                var instance = (BaseCommand)ActivatorUtilities.CreateInstance(_service, command);
                if (instance.Command != null)
                {
                    _cmdList.List.Add(instance.Command);
                    _command.AddHandler(instance.Command, new CommandInfo(instance.Execute)
                    {
                        HelpMessage = instance.HelpMessage
                    });
                }
                if (instance.CommandLiterate != null)
                {
                    foreach (var cmd in instance.CommandLiterate)
                    {
                        _cmdList.List.Add(cmd);
                        _command.AddHandler(cmd, new CommandInfo(instance.Execute)
                        {
                            HelpMessage = instance.HelpMessage
                        });
                    }
                }
            }
        }
    }
}