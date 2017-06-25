using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class Combo
    {
        public static void Run()
        {
            if (Menus.ComboMenu.Get<MenuCheckbox>("UseE").Checked)
            {
                AIHeroClient targetE = TargetSelector.GetTarget(SpellsManager.E.Range);
                if (SpellsManager.E.IsUsable() && targetE.IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.E.Cast(targetE);
                }
            }

            if (Menus.ComboMenu.Get<MenuCheckbox>("UseR").Checked)
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("UseQ").Checked && SpellsManager.Q.IsUsable()) return;
                if (Menus.ComboMenu.Get<MenuCheckbox>("UseE").Checked && SpellsManager.E.IsUsable()) return;

                if (SpellsManager.R.IsUsable() && Globals.MyHero.CountEnemiesInRange(SpellsManager.R.Range) >= Menus.ComboMenu.Get<MenuSlider>("MinEnemiesHitableR").CurrentValue) 
                {
                    SpellsManager.R.Cast();
                }
            } 
        }
    }
}
