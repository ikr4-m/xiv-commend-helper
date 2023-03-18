using System;
using System.Reflection;
using CommendMe.DataStructure;
using CommendMe.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace CommendMe.Services
{
    public class RegisterEventService : BaseService
    {
        private IServiceProvider _service;
        private EventList _evtList;

        public RegisterEventService(IServiceProvider service, EventList evtList)
        {
            _service = service;
            _evtList = evtList;
        }

        public override void Execute()
        {
            var listEvt = Assembly.GetExecutingAssembly().GetAssociatedNamespace<BaseEvents>("CommendMe.Events");

            foreach (var evt in listEvt)
            {
                var instance = (BaseEvents)ActivatorUtilities.CreateInstance(_service, evt);
                _evtList.List.Add(instance);
            }
        }
    }
}