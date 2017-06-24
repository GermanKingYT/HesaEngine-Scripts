using System;

using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_ANNIE.Modes;
using T2IN1_REBORN_ANNIE.Visuals;
using T2IN1_REBORN_ANNIE.Features;

using HesaEngine.SDK;

namespace T2IN1_REBORN_ANNIE.Managers
{
    internal class ModeManager
    {
        public static void Initialize()
        {
            Game.OnTick += Game_OnTick;

            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static int currentLevel = 0;

        private static void Game_OnTick()
        {
            /* TODO: TEMP TILL ON LEVEL UP IS FIXED */
            if (currentLevel != ObjectManager.Player.Level)
            {
                AutoLeveler.OnLevelUp(ObjectManager.Player.Level);
                currentLevel = ObjectManager.Player.Level;
            }

            Globals.CachedEnemies = Entities.GetEnemies;

            if (Globals.MyHero.IsDead || Globals.MyHero.IsRecalling() || Chat.IsChatOpen)
                return;

            PermActive.Initialize();

            if (Menus.MiscMenu.Get<MenuCheckbox>("KillSteal").Checked && Globals.Orb.ActiveMode != Orbwalker.OrbwalkingMode.Combo) { Features.Killsteal.Run(); }
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("EnableActivator").Checked) { Features.Activator.Run(); }

            switch (Globals.OrbwalkerMode)
            {
                case Orbwalker.OrbwalkingMode.Combo:
                {
                    Combo.Run();
                    break;
                }
                case Orbwalker.OrbwalkingMode.LaneClear:
                {
                    if (Menus.LaneClearMenu.Get<MenuSlider>("MaxMana").CurrentValue < Globals.MyHeroManaPercent)
                    {
                        LaneClear.Run();
                    }
                    break;
                }
                case Orbwalker.OrbwalkingMode.LastHit:
                {
                    if (Menus.LastHitMenu.Get<MenuSlider>("MaxMana").CurrentValue < Globals.MyHeroManaPercent)
                    {
                        LastHit.Run();
                    }
                    break;
                }
            }
        }
    }
}
