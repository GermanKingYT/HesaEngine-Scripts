using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal class LastHit
    {
        public static void Run()
        {
            if (Menus.LastHitMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
            {
                if (Globals.IsStunReady)
                {
                    return;
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseQ").Checked)
            {
                if (SpellSlot.Q.CanUseSpell())
                {
                    Obj_AI_Base _Target = Globals.GetLaneMinion(SpellsManager.Q);
                    if (_Target.IsValidTarget(SpellsManager.Q.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                    }
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (SpellSlot.W.CanUseSpell())
                {
                    Obj_AI_Base _Target = Globals.GetLaneMinion(SpellsManager.W);
                    if (_Target.IsValidTarget(SpellsManager.W.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.W.Cast(_Target));
                    }
                }
            }
        }
    }
}
