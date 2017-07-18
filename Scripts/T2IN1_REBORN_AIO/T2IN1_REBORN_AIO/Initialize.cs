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

        public void OnInitialize()
        {
            Game.OnGameLoaded += () =>
            {
                Library.Extensions.PluginList = new List<IPlugin> { new AutoLeveler() };

                Globals.RootMenu = Menu.AddMenu("[T2IN1-REBORN-AIO]");
                Globals.PluginMenu = Globals.RootMenu.AddSubMenu("PLUGINS");

                Library.Extensions.PluginList.ForEach(x => Globals.PluginMenu.AddSubMenu("Enabled").Add(new MenuCheckbox(x.Name, x.Name)).OnValueChanged += Library.Extensions.OnPluginStateChanged);

                switch (ObjectManager.Me.Hero)
                {
                    case Champion.Ashe:
                        Champions.Ashe.Initialize(); /* TODO: UNFINISHED */
                        return;
                    case Champion.Cassiopeia:
                        Champions.Cassiopeia.Initialize(); /* TODO: UNFINISHED */
                        return;
                    default:
                        Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO] ", ObjectManager.Player.ChampionName + " is not Supported!", "#e74c3c");
                        return;
                }
            };
        }
    }
}
