using System;
using System.Linq;

using T2IN1_REBORN_WUKONG.Managers;

using HesaEngine.SDK;
using SharpDX;

namespace T2IN1_REBORN_WUKONG.Visuals
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

                if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawE").Checked && Menus.VisualsMenu.Get<MenuCheckbox>("DrawOnlyWhenReadyE").Checked
                    ? SpellsManager.E.IsReady() : Menus.VisualsMenu.Get<MenuCheckbox>("DrawE").Checked) 
                {
                    Drawing.DrawCircle(Globals.MyHero.Position, SpellsManager.E.Range, Color.Yellow, 1);
                }

                if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawR").Checked && Menus.VisualsMenu.Get<MenuCheckbox>("DrawOnlyWhenReadyR").Checked
                    ? SpellsManager.R.IsReady() : Menus.VisualsMenu.Get<MenuCheckbox>("DrawR").Checked)
                {
                    Drawing.DrawCircle(Globals.MyHero.Position, SpellsManager.R.Range, Color.Red, 1);
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
