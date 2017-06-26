using System;

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
                if (!sender.IsMe || ObjectManager.Me.IsDead || Globals.IsUltimateActive) return;

                var target = ArgsTarget as Obj_AI_Base;
                if (target == null || !target.IsValid()) return;

                switch (Globals.OrbwalkerMode)
                {
                    case Orbwalker.OrbwalkingMode.Combo:
                    {
                        if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked && SpellsManager.Q.IsUsable())
                        {
                            SpellsManager.Q.Cast();
                            Orbwalker.ResetAutoAttackTimer();
                        }
                        else if (Menus.ComboMenu.Get<MenuCheckbox>("UseTiamat").Checked && SpellsManager.Tiamat.IsOwned() && SpellsManager.Tiamat.IsReady() && target.IsValidTarget(250))
                        {
                            SpellsManager.Tiamat.Cast();
                        }
                        else if (Menus.ComboMenu.Get<MenuCheckbox>("UseHydra").Checked && SpellsManager.Hydra.IsOwned() && SpellsManager.Hydra.IsReady() && target.IsValidTarget(250))
                        {
                            SpellsManager.Hydra.Cast();

                        }
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.JungleClear:
                    {
                        if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseQ").Checked && SpellsManager.Q.IsUsable())
                        {
                            SpellsManager.Q.Cast();
                            Orbwalker.ResetAutoAttackTimer();
                        }
                        else if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseTiamat").Checked && SpellsManager.Tiamat.IsOwned() && SpellsManager.Tiamat.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Tiamat.Cast();
                        }
                        else if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseHydra").Checked && SpellsManager.Hydra.IsOwned() && SpellsManager.Hydra.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Hydra.Cast();
                        }
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.LaneClear: 
                    {
                        if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseQ").Checked && SpellsManager.Q.IsUsable()) 
                        {
                            SpellsManager.Q.Cast();
                            Orbwalker.ResetAutoAttackTimer();
                        }
                        else if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseTiamat").Checked && SpellsManager.Tiamat.IsOwned() && SpellsManager.Tiamat.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Tiamat.Cast();
                        }
                        else if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseHydra").Checked && SpellsManager.Hydra.IsOwned() && SpellsManager.Hydra.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Hydra.Cast();
                        }
                        return;
                    }
                    case Orbwalker.OrbwalkingMode.LastHit: 
                    {
                        if (Menus.LastHitMenu.Get<MenuCheckbox>("UseQ").Checked && SpellsManager.Q.IsUsable()) 
                        {
                            SpellsManager.Q.Cast();
                            Orbwalker.ResetAutoAttackTimer();
                        }
                        else if (Menus.LastHitMenu.Get<MenuCheckbox>("UseTiamat").Checked && SpellsManager.Tiamat.IsOwned() && SpellsManager.Tiamat.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Tiamat.Cast();
                        }
                        else if (Menus.LastHitMenu.Get<MenuCheckbox>("UseHydra").Checked && SpellsManager.Hydra.IsOwned() && SpellsManager.Hydra.IsReady() && target.IsValidTarget(250)) 
                        {
                            SpellsManager.Hydra.Cast();
                        }
                        return;
                    }
                    default:
                        return;
                }
            };

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
