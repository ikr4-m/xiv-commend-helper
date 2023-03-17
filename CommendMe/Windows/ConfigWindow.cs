using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace CommendMe.Windows
{
    public class ConfigWindow : Window, IDisposable
    {
        public ConfigWindow() : base("Configuration", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.HorizontalScrollbar)
        {
            this.Size = new Vector2(500, 500);
            this.SizeCondition = ImGuiCond.Always;
        }

        public void Dispose() { }

        public override void Draw()
        {
            ImGui.Text("Hello world");
        }
    }
}