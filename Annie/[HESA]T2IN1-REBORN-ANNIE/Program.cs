using System;

using _HESA_T2IN1_REBORN_ANNIE;
using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN
{
    public class Program :IScript
    {
        public string Name => "[T2IN1-REBORN] Annie";
        public string Version => "1.1";
        public string Author => "RaINI";

        public void OnInitialize()
        {
            Game.OnGameLoaded += Game_OnGameLoaded;
        }

        private void Game_OnGameLoaded()
        {
            Chat.Print("<font color='#ecf0f1'>[T2IN1-REBORN] </font>Annie Version: 1.0");

            if (Globals.MyHero.Hero.Equals(Champion.Annie))
            {
                Console.Clear();

                try
                {
                    SpellsManager.Initialize();
                    Menus.Initialize();
                    Drawings.Initialize();
                    DamageIndicator.Initialize();
                    ModeManager.Initialize();
                    /* Interrupt.Initialize(); TODO: IMPLEMENT LATER AGAIN */
                }
                catch (Exception _Exception)
                {
                    Logger.Log("Error: " + _Exception, ConsoleColor.Red);
                }

                Chat.Print("<font color='#27ae60'>[T2IN1-REBORN] </font>" + Globals.MyHero.ChampionName + " Script is ready");
            }
            else
            {
                Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN] </font> Champion: " + ObjectManager.Player.ChampionName + " is not Supported!");
            }
        }
    }
}
