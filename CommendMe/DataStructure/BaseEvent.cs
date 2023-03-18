using System;
using System.Collections.Generic;

namespace CommendMe.DataStructure
{
    public abstract class BaseEvents : IDisposable
    {
        public abstract void Dispose();
    }

    public class EventList : IDisposable
    {
        public List<BaseEvents> List = new();

        public void Dispose()
        {
            foreach (var list in List) list.Dispose();
        }
    }
}