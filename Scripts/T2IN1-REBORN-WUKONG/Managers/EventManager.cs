using System;

using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Managers
{
    internal class EventManager
    {
        public static void Initialize()
        {
            Orbwalker.AfterAttack += (sender, ArgsTarget) =>
            {
                if (!sender.IsMe || ObjectManager.Me.IsDead || !SpellsManager.Q.IsUsable()) return;

                var target = ArgsTarget as Obj_AI_Base;
                if (target == null || !target.IsValidTarget()) return;

                switch (Globals.OrbwalkerMode)
                {
                    case Orbwalker.OrbwalkingMode.Combo:
                    {
                        if (!Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked) return;
                        break;
                    }
                    case Orbwalker.OrbwalkingMode.JungleClear:
                    {
                        if (!Menus.JungleClearMenu.Get<MenuCheckbox>("UseQ").Checked) return;
                        break;
                    }
                    case Orbwalker.OrbwalkingMode.LaneClear: 
                    {
                        if (!Menus.LaneClearMenu.Get<MenuCheckbox>("UseQ").Checked) return;
                        break;
                    }
                    case Orbwalker.OrbwalkingMode.LastHit: 
                    {
                        if (!Menus.LastHitMenu.Get<MenuCheckbox>("UseQ").Checked) return;
                        break;
                    }
                    default:
                        return;
                }

                SpellsManager.Q.Cast(); Orbwalker.ResetAutoAttackTimer();
            };

            Obj_AI_Base.OnBuffGained += (sender, args) =>
            {
                if (!sender.IsMe) return;

                if (args.Buff.Name.ToLower().Equals("monkeykingspintowin"))
                {
                    Globals.IsUltimateActive = true;
                }
            };

            Obj_AI_Base.OnBuffLost += (sender, args) =>
            {
                if (!sender.IsMe) return;

                if (args.Buff.Name.ToLower().Equals("monkeykingspintowin")) 
                {
                    Globals.IsUltimateActive = true;
                }
            };

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
