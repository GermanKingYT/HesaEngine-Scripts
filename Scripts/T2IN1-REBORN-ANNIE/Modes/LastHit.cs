using T2IN1_REBORN_LIB;
using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_ANNIE.Managers;
using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_ANNIE.Modes
{
    internal class LastHit
    {
        public static void Run()
        {
            if (Menus.LastHitMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
            {
                if (Globals.IsPassiveReady) return;
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseQ").Checked)
            {
                if (SpellsManager.Q.IsUsable())
                {
                    Obj_AI_Base target = Globals.GetLaneMinion(SpellsManager.Q);

                    if (target.IsValidTarget(SpellsManager.Q.Range))
                    {
                        SpellsManager.Q.Cast(target);
                    }
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (!SpellsManager.W.IsUsable()) return;

                Obj_AI_Base target = Globals.GetLaneMinion(SpellsManager.W);

                if (target.IsValidTarget(SpellsManager.W.Range))
                {
                    SpellsManager.W.Cast(target);
                }
            }
        }
    }
}
