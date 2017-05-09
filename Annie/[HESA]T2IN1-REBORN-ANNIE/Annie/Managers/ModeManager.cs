using System;

using _HESA_T2IN1_REBORN.Champions.Annie.Visuals;

using HesaEngine.SDK;

namespace _HESA_T2IN1_REBORN.Champions.Annie.Managers
{
    internal static class ModeManager
    {
        public static void Initialize()
        {
            Game.OnTick += _Game_OnTick;
            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static void _Game_OnTick()
        {
            if (Globals.MyHero.IsDead || Globals.MyHero.IsRecalling() || Chat.IsChatOpen)
                return;

            var _OrbwalkerMode = Globals.Orb.ActiveMode;
            var _MyManaPercent = Globals.MyHero.ManaPercent;

            /* PERMA ACTIVE */
            Modes.PermActive.Initialize();

            switch (_OrbwalkerMode)
            {
                case Orbwalker.OrbwalkingMode.Combo:
                {
                    Modes.Combo.Run();
                    break;
                }

                case Orbwalker.OrbwalkingMode.LaneClear:
                {
                    if (Menus.LaneClearMenu.Get<MenuSlider>("MaxMana").CurrentValue < _MyManaPercent)
                    {
                        Modes.LaneClear.Run();
                    }
                    break;
                }

                case Orbwalker.OrbwalkingMode.LastHit:
                {
                    if (Menus.LastHitMenu.Get<MenuSlider>("MaxMana").CurrentValue < _MyManaPercent)
                    {
                        Modes.LastHit.Run();
                    }
                    break;
                }
            }
        }
    }
}
