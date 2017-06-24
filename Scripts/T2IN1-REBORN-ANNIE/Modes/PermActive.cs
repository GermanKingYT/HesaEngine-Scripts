using T2IN1_REBORN_LIB;
using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_ANNIE.Managers;
using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;

namespace T2IN1_REBORN_ANNIE.Modes
{
    internal class PermActive
    {
        public static void Initialize()
        {
            /* Tibbers Auto Control */
            if (Globals.IsTibbersSpawned)
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("AutoControl").Checked)
                {
                    Tibbers.TibbersMethod();
                }
            }

            /* Auto Stack Passive */
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoStackPassive").Checked && !Globals.MyHero.IsRecalling())
            {
                if (Globals.IsPassiveReady) return;

                if (Globals.MyHero.InFountain() && Globals.MyHeroManaPercent > Menus.MiscMenu.Get<MenuSlider>("StackPassiveManaSpawn").CurrentValue)
                {
                    if (SpellsManager.E.IsUsable())
                    {
                        SpellsManager.E.Cast();
                    }

                    if (SpellsManager.W.IsUsable())
                    {
                        SpellsManager.W.Cast(Globals.MyHero.Position);
                    }
                }
                else
                {
                    if (!(Globals.MyHeroManaPercent > Menus.MiscMenu.Get<MenuSlider>("StackPassiveMana").CurrentValue)) return;

                    if (SpellsManager.E.IsUsable())
                    {
                        SpellsManager.E.Cast();
                    }
                }
            }
        }
    }
}
