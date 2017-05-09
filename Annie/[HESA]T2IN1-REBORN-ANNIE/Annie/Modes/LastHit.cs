using _HESA_T2IN1_REBORN.Champions.Annie.Managers;
using _HESA_T2IN1_REBORN.Champions.Annie.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN.Champions.Annie.Modes
{
    internal class LastHit
    {
        public static void Run()
        {
            if (Menus.LastHitMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
            {
                if (Globals.IsStunReady())
                {
                    return;
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseQ").Checked)
            {
                if (Globals.CanUseSpell(SpellSlot.Q))
                {
                    var _Target = Globals.GetLaneMinion(SpellsManager.Q);
                    if (Globals.IsObjectValidWithRange(_Target, SpellsManager.Q.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                    }
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (Globals.CanUseSpell(SpellSlot.W))
                {
                    var _Target = Globals.GetLaneMinion(SpellsManager.W);
                    if (Globals.IsObjectValidWithRange(_Target, SpellsManager.W.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.W.Cast(_Target));
                    }
                }
            }
        }
    }
}
