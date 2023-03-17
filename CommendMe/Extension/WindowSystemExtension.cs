using System.Linq;
using Dalamud.Interface.Windowing;

namespace CommendMe.Extension
{
    public static class WindowManagerExtension
    {
        public static void OpenWindow<T>(this WindowSystem windowSystem)
        {
            var w = windowSystem.Windows.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
            if (w != null) w.IsOpen = true;
        }
    }
}