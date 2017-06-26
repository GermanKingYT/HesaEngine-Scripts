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
                Obj_AI_Base target = SpellsManager.E.GetBestTarget();
                if (SpellsManager.E.IsUsable() && target.IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.E.Cast(target);
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
