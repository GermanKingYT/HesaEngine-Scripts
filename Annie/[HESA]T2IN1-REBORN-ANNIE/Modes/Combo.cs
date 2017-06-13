using System.Collections.Generic;
using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal class Combo
    {
        private static AIHeroClient TargetQ;
        private static AIHeroClient TargetW;
        private static AIHeroClient TargetR;

        private static Dictionary<Vector2, int> PredictionR;
        private static Vector3 PredictedRPosition;

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
            TargetQ = TargetSelector.GetTarget(SpellsManager.Q.Range);
            TargetW = TargetSelector.GetTarget(SpellsManager.W.Range);
            TargetR = TargetSelector.GetTarget(SpellsManager.R.Range);

            if (Menus.ComboMenu.Get<MenuCheckbox>("UseE").Checked && Globals.MyHero.CountEnemiesInRange(1000) > 0)
            {
                if (SpellSlot.E.CanUseSpell())
                {
                    if (!Globals.IsStunReady)
                    {
                        Globals.DelayAction(() => SpellsManager.E.Cast());
                    }
                }
            }

            if (Menus.ComboMenu.Get<MenuCheckbox>("OnlyIfStunReadyR").Checked && SpellSlot.R.CanUseSpell() && !Globals.IsTibbersSpawned)
            {
                if (Globals.IsStunReady)
                {
                    if (TargetR.IsValidTarget(SpellsManager.R.Range))
                    {
                        PredictionR = Other.Prediction.GetBestUltimatePosition(TargetR.ServerPosition.To2D());
                        if (PredictionR.Values.First() >= Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue)
                        {
                            PredictedRPosition = PredictionR.Keys.First().To3D();
                            if (PredictedRPosition != Vector3.Zero)
                            {
                                SpellsManager.R.Cast(PredictedRPosition);
                            }
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked && !Globals.IsStunReady)
                {
                    if (SpellSlot.Q.CanUseSpell())
                    {
                        if (TargetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.Q.Cast(TargetQ));
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked && !Globals.IsStunReady)
                {
                    if (SpellSlot.W.CanUseSpell())
                    {
                        if (TargetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.W.CastOnUnit(TargetW));
                        }
                    }
                }
            }
            else
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("UseR").Checked && !Globals.IsTibbersSpawned)
                {
                    if (SpellSlot.R.CanUseSpell())
                    {
                        if (TargetR.IsValidTarget(SpellsManager.R.Range))
                        {
                            PredictionR = Other.Prediction.GetBestUltimatePosition(TargetR.ServerPosition.To2D());
                            if (PredictionR.Values.First() >= Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue)
                            {
                                PredictedRPosition = PredictionR.Keys.First().To3D();
                                if (PredictedRPosition != Vector3.Zero)
                                {
                                    SpellsManager.R.Cast(PredictedRPosition);
                                }
                            }
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked)
                {
                    if (SpellSlot.Q.CanUseSpell())
                    {
                        if (TargetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.Q.Cast(TargetQ));
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked)
                {
                    if (SpellSlot.W.CanUseSpell())
                    {
                        if (TargetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.W.CastOnUnit(TargetW));
                        }
                    }
                }  
            }
        }
    }
}
