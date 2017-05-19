using System;
using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE.Visuals
{
    internal class Drawings
    {
        public static void Initialize()
        {
            Drawing.OnDraw += _Drawing_OnDraw;
            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static void _Drawing_OnDraw(EventArgs args)
        {
            if (Globals.MyHero.IsDead)
                return;

            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawSpellsRange").Checked)
            {
                if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawQ").Checked && Menus.VisualsMenu.Get<MenuCheckbox>("DrawOnlyWhenReadyQ").Checked
                    ? SpellsManager.Q.IsReady() : Menus.VisualsMenu.Get<MenuCheckbox>("DrawQ").Checked)
                {
                    Drawing.DrawCircle(Globals.MyHero.Position, SpellsManager.Q.Range, Color.BlueViolet, 1);
                }

                if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawW").Checked && Menus.VisualsMenu.Get<MenuCheckbox>("DrawOnlyWhenReadyW").Checked
                    ? SpellsManager.W.IsReady() : Menus.VisualsMenu.Get<MenuCheckbox>("DrawW").Checked)
                {
                    Drawing.DrawCircle(Globals.MyHero.Position, SpellsManager.W.Range, Color.Yellow, 1);
                }

                if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawR").Checked && Menus.VisualsMenu.Get<MenuCheckbox>("DrawOnlyWhenReadyR").Checked
                    ? SpellsManager.R.IsReady() : Menus.VisualsMenu.Get<MenuCheckbox>("DrawR").Checked)
                {
                    Drawing.DrawCircle(Globals.MyHero.Position, SpellsManager.R.Range, Color.Red, 1);
                }
            }

            /* TODO: OPTIMIZE */
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawPredictionR").Checked)
            {
                if (SpellSlot.R.CanUseSpell() && !Globals.IsTibbersSpawned)
                {
                    var _Target = TargetSelector.GetTarget(SpellsManager.R.Range);
                    if (_Target.IsValidTarget(625))
                    {
                        var _PositionAndHits = Other.Prediction.GetBestUltimatePosition(_Target.ServerPosition.To2D());
                        if (_PositionAndHits.First().Value >= Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue)
                        {
                            if (Menus.ComboMenu.Get<MenuCheckbox>("OnlyIfStunReadyR").Checked)
                            {
                                if (Globals.IsStunReady)
                                {
                                    Drawing.DrawCircle(_PositionAndHits.First().Key.To3DWorld(), 150, Color.OrangeRed);
                                    Drawing.DrawLine(Globals.MyHero.Position, _PositionAndHits.First().Key.To3DWorld(), Color.OrangeRed, 2);
                                }
                            }
                            else
                            {
                                Drawing.DrawCircle(_PositionAndHits.First().Key.To3DWorld(), 150, Color.OrangeRed);
                                Drawing.DrawLine(Globals.MyHero.Position, _PositionAndHits.First().Key.To3DWorld(), Color.OrangeRed, 2);
                            } 
                        }
                    }
                }
            }

            /* TODO: OPTIMIZE */
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawPredictionW").Checked && Globals.OrbwalkerMode != Orbwalker.OrbwalkingMode.Combo)
            {
                if (SpellSlot.W.CanUseSpell())
                {
                    Vector3 _PredictionW = new Vector3();
                    if (Other.Prediction.GetBestLocationW(GameObjectType.obj_AI_Minion, out _PredictionW) >= Menus.LaneClearMenu.Get<MenuSlider>("MinMinions").CurrentValue)
                    {
                        if (_PredictionW != Vector3.Zero)
                        {
                            if (Menus.LaneClearMenu.Get<MenuSlider>("MaxMana").CurrentValue < Globals.MyHeroManaPercent)
                            {
                                if (Menus.LaneClearMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
                                {
                                    if (!Globals.IsStunReady)
                                    {
                                        Drawing.DrawLine(Globals.MyHero.Position, _PredictionW, Color.GreenYellow, 2);
                                        Drawing.DrawCircle(_PredictionW, 70, Color.GreenYellow);
                                    }
                                }
                                else
                                {
                                    Drawing.DrawLine(Globals.MyHero.Position, _PredictionW, Color.GreenYellow, 2);
                                    Drawing.DrawCircle(_PredictionW, 70, Color.GreenYellow);
                                }
                            }
                        }
                    }
                }
            }

            /* TODO: OPTIMIZE */
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawBoundingRadius").Checked)
            {
                foreach (var _Enemy in ObjectManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsVisibleOnScreen))
                {
                    Drawing.DrawCircle(_Enemy.Position, _Enemy.BoundingRadius);
                }
            }
        }
    }
}
