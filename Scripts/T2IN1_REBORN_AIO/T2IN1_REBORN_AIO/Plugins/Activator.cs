using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;

namespace T2IN1_REBORN_AIO.Plugins
{
    internal class Activator : IPlugin
    {
        public string Name => "Activator";
        public bool Initialized { get; set; }
        public Menu Menu { get; set; }

        public void Initialize()
        {
            if (Initialized) return;

            Menu = Globals.PluginMenu.AddSubMenu(Name);

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Activator Plugin Loaded", "#27ae60");

            Initialized = true;
        }

        public void Unload()
        {
            if (!Initialized) return;

            Menu.RemoveMenu();

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Activator Plugin Unloaded", "#e74c3c");

            Initialized = false;
        }
    }
}
