using System;

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
            Game.OnTick += Game_OnTick;

            Orbwalker.AfterAttack += (sender, ArgsTarget) =>
            {
                if (!sender.IsMe || ObjectManager.Me.IsDead) return;

                var target = ArgsTarget as AIHeroClient;
                if (target == null || !target.IsValidTarget() || !SpellsManager.Q.IsUsable()) return;

                SpellsManager.Q.Cast();
                Orbwalker.ResetAutoAttackTimer();
            };

            Logger.Log(">> Executed", ConsoleColor.Green);
        }

       

        private static void Game_OnTick()
        {
            /* TODO: TEMP TILL ON LEVEL UP IS FIXED */
            AutoLeveler.TempFix();

            Globals.CachedEnemies = Entities.GetEnemies;

            if (Globals.MyHero.IsDead || Globals.MyHero.IsRecalling() || Chat.IsChatOpen)
                return;

            PermActive.Initialize();

            if (Menus.MiscMenu.Get<MenuCheckbox>("KillSteal").Checked && Globals.Orb.ActiveMode != Orbwalker.OrbwalkingMode.Combo) { Killsteal.Run(); }
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
