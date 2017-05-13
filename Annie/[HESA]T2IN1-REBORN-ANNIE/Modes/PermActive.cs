using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal class PermActive
    {
        public static void Initialize()
        {
            /* Tibbers Auto Control */
            Globals.IsTibbersSpawned = Globals.MyHero.GetSpell(SpellSlot.R).SpellData.Name.Equals("InfernalGuardianGuide");
            if (Globals.IsTibbersSpawned)
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("AutoControl").Checked)
                {
                    Modes.Tibbers.TibbersMethod();
                }
            }

            /* Auto Stack Passive */
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoStackPassive").Checked && !Globals.MyHero.IsRecalling())
            {
                if (!Globals.IsStunReady)
                {
                    if (Globals.MyHero.InFountain() && Globals.MyHeroManaPercent > Menus.MiscMenu.Get<MenuSlider>("StackPassiveManaSpawn").CurrentValue)
                    {
                        if (SpellSlot.E.CanUseSpell())
                        {
                            Globals.DelayAction(() => SpellsManager.E.Cast());
                        }

                        if (SpellSlot.W.CanUseSpell())
                        {
                            Globals.DelayAction(() => SpellsManager.W.Cast());
                        }
                    }
                    else
                    {
                        if (Globals.MyHeroManaPercent > Menus.MiscMenu.Get<MenuSlider>("StackPassiveMana").CurrentValue)
                        {
                            if (SpellSlot.E.CanUseSpell())
                            {
                                Globals.DelayAction(() => SpellsManager.E.Cast());
                            }
                        }
                    }
                }
            }
        }
    }
}
