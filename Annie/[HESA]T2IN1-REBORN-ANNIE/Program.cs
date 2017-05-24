using System;

using _HESA_T2IN1_REBORN_ANNIE;
using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using _HESA_T2IN1_REBORN_ANNIE.Other;

namespace _HESA_T2IN1_REBORN
{
    public class Program : IScript
    {
        public string Name => "[T2IN1-REBORN] Annie";
        public string Version => "1.4.2";
        public string Author => "RaINi";

        public void OnInitialize()
        {
            Game.OnGameLoaded += Game_OnGameLoaded;
        }

        private static void LoadScript()
        {
            Console.Clear();

            try
            {
                SpellsManager.Initialize();
                Menus.Initialize();
                Drawings.Initialize();
                DamageIndicator.Initialize();
                ModeManager.Initialize();
                /* Interrupt.Initialize(); TODO: FINISH */
            }
            catch (Exception _Exception)
            {
                Logger.Log("Error: " + _Exception, ConsoleColor.Red);
            }

            Chat.Print("<font color='#27ae60'>[T2IN1-REBORN] </font>Script is fully initialized");
        }

        private void Game_OnGameLoaded()
        {
            if (Globals.MyHero.Hero.Equals(Champion.Annie))
            {
                switch (Updater.Run(Name, Version))
                {
                    case "NoUpdate":
                        Chat.Print("<font color='#27ae60'>[T2IN1-UPDATE-CHECKER] </font>No update found");
                        break;
                    case "Failed":
                        Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>Could not check for updates");
                        break;
                    case "NewVersion":
                        Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>A new update is available");
                        break;
                    case "Updated":
                        Chat.Print("<font color='#27ae60'>[T2IN1-UPDATE-CHECKER] </font>Reload the script to use the new version");
                        break;
                    case "DownloadFailed":
                        Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>Download failed");
                        break;
                    default:
                        Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>Could not check for updates");
                        break;
                }

                LoadScript();
            }
            else
            {
                Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN] </font> Champion: " + ObjectManager.Player.ChampionName + " is not Supported!");
            }
        }
    }
}
