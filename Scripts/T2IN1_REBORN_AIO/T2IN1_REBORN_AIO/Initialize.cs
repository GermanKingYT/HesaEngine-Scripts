using System.Collections.Generic;

using T2IN1_REBORN_AIO.Plugins;
using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace T2IN1_REBORN_AIO
{
    public class Initialize : IScript
    {
        public string Name => "T2IN1-REBORN-AIO";
        public string Version => "1.0.0";
        public string Author => "RaINi";

        private void OnPluginStateChanged(MenuCheckbox menuCheckbox, bool active)
        {
            IPlugin plugin = Library.Extensions.PluginList.Find(x => x.Name == menuCheckbox.Name);
            if (plugin == null) return;

            switch (active)
            {
                case true:
                    plugin.Initialize();
                    return;
                case false:
                    plugin.Unload();
                    return;
            }
        }

        public void OnInitialize()
        {
            Game.OnGameLoaded += () =>
            {
                Library.Extensions.PluginList = new List<IPlugin> { new Testing() };

                Globals.RootMenu = Menu.AddMenu("[T2IN1-REBORN-AIO]");
                Globals.PluginMenu = Globals.RootMenu.AddSubMenu("PLUGINS");

                Library.Extensions.PluginList.ForEach(x => Globals.PluginMenu.AddSubMenu("Enabled").Add(new MenuCheckbox(x.Name, x.Name)).OnValueChanged += OnPluginStateChanged);

                switch (ObjectManager.Me.Hero)
                {
                    case Champion.Ahri:
                        Champions.Ahri.Initialize();
                        return;
                    default:
                        Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO] ", ObjectManager.Player.ChampionName + " is not Supported!", "#e74c3c");
                        return;
                }
            };
        }
    }
}
