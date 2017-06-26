using System;
using System.Linq;
using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Modes;
using T2IN1_REBORN_WUKONG.Visuals;
using T2IN1_REBORN_WUKONG.Features;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Managers
{
    internal class ModeManager
    {
        public static void Initialize()
        {
            Game.OnTick += () =>
            {
                /* TODO: TEMP TILL ON LEVEL UP IS FIXED */
                AutoLeveler.TempFix();

                Globals.CachedEnemies = Entities.GetEnemies;

                if (Globals.MyHero.IsDead || Globals.MyHero.IsRecalling() || Chat.IsChatOpen)
                    return;

                if (Globals.IsUltimateActive) return;

                switch (Globals.OrbwalkerMode) 
                {
                    case Orbwalker.OrbwalkingMode.Combo: 
                    {
                        Combo.Run();
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.LaneClear: 
                    {
                        if (Menus.LaneClearMenu.Get<MenuSlider>("MaxMana").CurrentValue <= Globals.MyHeroManaPercent) 
                        {
                            LaneClear.Run();
                        }
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.LastHit: 
                    {
                        if (Menus.LastHitMenu.Get<MenuSlider>("MaxMana").CurrentValue <= Globals.MyHeroManaPercent) 
                        {
                            LastHit.Run();
                        }
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.JungleClear:
                    {
                        if (Menus.JungleClearMenu.Get<MenuSlider>("MaxMana").CurrentValue <= Globals.MyHeroManaPercent) 
                        {
                            JungleClear.Run();
                        }
                        return;
                    }
                }
            };

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
