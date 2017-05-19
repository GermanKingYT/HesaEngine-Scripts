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
        private static AIHeroClient _TargetQ;
        private static AIHeroClient _TargetW;
        private static AIHeroClient _TargetR;

        private static Dictionary<Vector2, int> _PredictionR;
        private static Vector3 _PredictedRPosition;

        public static void Run()
        {
            _ComboManager();
        }

        private static void _ComboManager()
        {
            _Combo();
        }

        private static void _Combo()
        {
            _TargetQ = TargetSelector.GetTarget(SpellsManager.Q.Range);
            _TargetW = TargetSelector.GetTarget(SpellsManager.W.Range);
            _TargetR = TargetSelector.GetTarget(SpellsManager.R.Range);

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

            if (Menus.ComboMenu.Get<MenuCheckbox>("OnlyIfStunReadyR").Checked && SpellSlot.R.CanUseSpell())
            {
                if (Globals.IsStunReady)
                {
                    if (_TargetR.IsValidTarget(SpellsManager.R.Range) && !Globals.IsTibbersSpawned)
                    {
                        _PredictionR = Other.Prediction.GetBestUltimatePosition(_TargetR.ServerPosition.To2D());
                        if (_PredictionR.Values.First() >= Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue)
                        {
                            _PredictedRPosition = _PredictionR.Keys.First().To3D();
                            if (_PredictedRPosition != Vector3.Zero)
                            {
                                SpellsManager.R.Cast(_PredictedRPosition);
                            }
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked)
                {
                    if (SpellSlot.Q.CanUseSpell() && !Globals.IsStunReady)
                    {
                        if (_TargetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.Q.Cast(_TargetQ));
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked)
                {
                    if (SpellSlot.W.CanUseSpell() && !Globals.IsStunReady)
                    {
                        if (_TargetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_TargetW));
                        }
                    }
                }
            }
            else
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("UseR").Checked)
                {
                    if (SpellSlot.R.CanUseSpell())
                    {
                        if (_TargetR.IsValidTarget(SpellsManager.R.Range) && !Globals.IsTibbersSpawned)
                        {
                            _PredictionR = Other.Prediction.GetBestUltimatePosition(_TargetR.ServerPosition.To2D());
                            if (_PredictionR.Values.First() >= Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue)
                            {
                                _PredictedRPosition = _PredictionR.Keys.First().To3D();
                                if (_PredictedRPosition != Vector3.Zero)
                                {
                                    SpellsManager.R.Cast(_PredictedRPosition);
                                }
                            }
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked)
                {
                    if (SpellSlot.Q.CanUseSpell())
                    {
                        if (_TargetQ.IsValidTarget(SpellsManager.Q.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.Q.Cast(_TargetQ));
                        }
                    }
                }

                if (Menus.ComboMenu.Get<MenuCheckbox>("UseW").Checked)
                {
                    if (SpellSlot.W.CanUseSpell())
                    {
                        if (_TargetW.IsValidTarget(SpellsManager.W.Range))
                        {
                            Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_TargetW));
                        }
                    }
                }  
            }
        }
    }
}
