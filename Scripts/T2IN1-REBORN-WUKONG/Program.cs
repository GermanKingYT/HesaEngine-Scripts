using System;

using T2IN1_REBORN_LIB.Features;

using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace T2IN1_REBORN_WUKONG
{
    public class Program : IScript
    {
        public string Name => "[T2IN1-REBORN] Wukong";
        public string Version => "1.0.0";
        public string Author => "RaINi";

        public void OnInitialize()
        {
            Game.OnGameLoaded += () =>
            {
                if (Globals.MyHero.Hero.Equals(Champion.MonkeyKing))
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
                        default:
                            Chat.Print("<font color='#e74c3c'>[T2IN1-UPDATE-CHECKER] </font>Could not check for updates");
                            break;
                    }

                    Console.Clear();

                    try
                    {
                        SpellsManager.Initialize();
                        Menus.Initialize();
                        Drawings.Initialize();
                        DamageIndicator.Initialize();
                        ModeManager.Initialize();
                    }
                    catch (Exception exception) 
                    {
                        Logger.Log("Error: " + exception, ConsoleColor.Red);
                    }

                    Chat.Print("<font color='#27ae60'>[T2IN1-REBORN] </font>Script is fully initialized");
                }
                else 
                {
                    Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN] </font> Champion: " + ObjectManager.Player.ChampionName + " is not Supported!");
                }
            };
        }
    }
}
