using _HESA_T2IN1_REBORN.Champions.Annie;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN
{
    public class Program :IScript
    {
        public string Name => "[T2IN1] REBORN AIO";
        public string Version => "1.0";
        public string Author => "RaINI";

        public void OnInitialize()
        {
            Game.OnGameLoaded += Game_OnGameLoaded;
        }

        private void Game_OnGameLoaded()
        {
            Chat.Print("<font color='#ecf0f1'>[T2IN1-REBORN] </font>AIO Version: 1.0");

            if (Globals.MyHero.Hero.Equals(Champion.Annie))
            {
                Initialize.Run();
            }
            else
            {
                Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN] </font>" + ObjectManager.Player.ChampionName + " is not Supported!");
            }
        }
    }
}
