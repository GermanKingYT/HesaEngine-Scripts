using System;
using System.Linq;

using T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;

namespace T2IN1_REBORN_ANNIE.Visuals
{
    internal class Drawings
    {
        public static void Initialize()
        {
            Drawing.OnDraw += Drawing_OnDraw;

            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static void Drawing_OnDraw(EventArgs args)
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
                if (SpellsManager.R.IsUsable() && !Globals.IsTibbersSpawned)
                {
                    Vector3 bestCastPosition = Other.Prediction.GetBestCircularCastPosition(GameObjectType.AIHeroClient, SpellsManager.R, Menus.ComboMenu.Get<MenuSlider>("UltimateTargets").CurrentValue, 235);

                    if (bestCastPosition != Vector3.Zero) 
                    {
                        if (Menus.ComboMenu.Get<MenuCheckbox>("OnlyIfStunReadyR").Checked) 
                        {
                            if (Globals.IsPassiveReady)
                            {
                                Drawing.DrawCircle(bestCastPosition, 235, Color.OrangeRed);
                                Drawing.DrawLine(Globals.MyHero.Position, bestCastPosition, Color.OrangeRed, 2);
                            }
                        }
                        else 
                        {
                            Drawing.DrawCircle(bestCastPosition, 235, Color.OrangeRed);
                            Drawing.DrawLine(Globals.MyHero.Position, bestCastPosition, Color.OrangeRed, 2);
                        }
                    }
                }
            }

            /* TODO: OPTIMIZE */
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawPredictionW").Checked && Globals.OrbwalkerMode != Orbwalker.OrbwalkingMode.Combo)
            {
                if (SpellsManager.W.IsUsable())
                {
                    Vector3 predictionW = new Vector3();

                    if (Other.Prediction.GetBestLocationW(out predictionW) >= Menus.LaneClearMenu.Get<MenuSlider>("MinMinions").CurrentValue)
                    {
                        if (predictionW != Vector3.Zero)
                        {
                            if (Menus.LaneClearMenu.Get<MenuSlider>("MaxMana").CurrentValue < Globals.MyHeroManaPercent)
                            {
                                if (Menus.LaneClearMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
                                {
                                    if (!Globals.IsPassiveReady)
                                    {
                                        Drawing.DrawLine(Globals.MyHero.Position, predictionW, Color.GreenYellow, 2);
                                        Drawing.DrawCircle(predictionW, 70, Color.GreenYellow);
                                    }
                                }
                                else
                                {
                                    Drawing.DrawLine(Globals.MyHero.Position, predictionW, Color.GreenYellow, 2);
                                    Drawing.DrawCircle(predictionW, 70, Color.GreenYellow);
                                }
                            }
                        }
                    }
                }
            }

            /* TODO: OPTIMIZE */
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawBoundingRadius").Checked)
            {
                if (Globals.CachedEnemies == null || !Globals.CachedEnemies.Any()) return;

                Globals.CachedEnemies.Where(x => !x.IsDead && x.IsVisibleOnScreen && x.IsVisible).ToList().ForEach(x => Drawing.DrawCircle(x.Position, x.BoundingRadius));
            }
        }
    }
}
