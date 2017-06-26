using T2IN1_REBORN_ANNIE.Managers;
using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_ANNIE.Modes
{
    internal class Combo
    {
        public static void Run()
        {
            ComboManager();
        }

        private static void ComboManager()
        {
            DoCombo();
        }

        private static void DoCombo()
        {
            AIHeroClient targetQ = TargetSelector.GetTarget(SpellsManager.Q.Range);
            AIHeroClient targetW = TargetSelector.GetTarget(SpellsManager.W.Range);
            AIHeroClient targetR = TargetSelector.GetTarget(SpellsManager.R.Range);

            if (Menus.ComboMenu.Get<MenuCheckbox>("UseE").Checked && Globals.MyHero.CountEnemiesInRange(1000) > 0)
            {
                if (SpellsManager.E.IsUsable())
                {
                    if (!Globals.IsPassiveReady)
                    {
                        SpellsManager.E.Cast();
                    }
                }
            }

            if (Menus.ComboMenu.Get<MenuCheckbox>("OnlyIfStunReadyR").Checked && SpellsManager.R.IsUsable() && !Globals.IsTibbersSpawned)
            {
                if (Globals.IsPassiveReady)
                {
                    if (targetR.IsValidTarget(SpellsManager.R.Range))
                    {
                        Vector3 bestCastPosition = Other.Prediction.GetBestCircularCastPosition(GameObjectType.AIHeroClient, SpellsManager.R, Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue, 235);
                        if (bestCastPosition != Vector3.Zero)
                        {
                            SpellsManager.R.Cast(bestCastPosition);
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked && !Globals.IsPassiveReady)
                {
                    if (SpellsManager.Q.IsUsable())
                    {
                        if (targetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            SpellsManager.Q.Cast(targetQ);
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked && !Globals.IsPassiveReady)
                {
                    if (SpellsManager.W.IsUsable())
                    {
                        if (targetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            SpellsManager.W.CastOnUnit(targetW);
                        }
                    }
                }
            }
            else
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("UseR").Checked && !Globals.IsTibbersSpawned)
                {
                    if (SpellsManager.R.IsUsable())
                    {
                        if (targetR.IsValidTarget(SpellsManager.R.Range))
                        {
                            Vector3 bestCastPosition = Other.Prediction.GetBestCircularCastPosition(GameObjectType.AIHeroClient, SpellsManager.R, Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue, 235);
                            if (bestCastPosition != Vector3.Zero) 
                            {
                                SpellsManager.R.Cast(bestCastPosition);
                            }
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked)
                {
                    if (SpellsManager.Q.IsUsable())
                    {
                        if (targetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            SpellsManager.Q.Cast(targetQ);
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked)
                {
                    if (SpellsManager.W.IsUsable())
                    {
                        if (targetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            SpellsManager.W.CastOnUnit(targetW);
                        }
                    }
                }  
            }
        }
    }
}
