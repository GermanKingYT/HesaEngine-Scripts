using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;

namespace T2IN1_REBORN_AIO.Plugins
{
    internal class Testing : IPlugin
    {
        public string Name => "Testing Plugin";
        public bool Initialized { get; set; }
        public Menu Menu { get; set; }

        public void Initialize()
        {
            if (Initialized) return;

            Menu = Globals.PluginMenu.AddSubMenu(Name);

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Test Plugin Loaded", "#27ae60");

            Chat.OnChatMessage += OnMessage;

            Initialized = true;
        }

        public static void OnMessage(string s)
        {
            Logger.Log("[OnMessage] Message:" + s);
        }

        public void Unload()
        {
            if (!Initialized) return;

            Menu.RemoveMenu();

            Chat.OnChatMessage -= OnMessage;

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Test Plugin Unloaded", "#e74c3c");

            Initialized = false;
        }
    }
}
