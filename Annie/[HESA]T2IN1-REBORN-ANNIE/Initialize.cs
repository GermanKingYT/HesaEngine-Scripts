using System;

using _HESA_T2IN1_REBORN_ANNIE.Features;
using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;

namespace _HESA_T2IN1_REBORN_ANNIE
{
    internal class Initialize
    {
        public static void Run()
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
    }        
}
