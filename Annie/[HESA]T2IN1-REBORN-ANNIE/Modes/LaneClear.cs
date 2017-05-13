using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal class LaneClear
    {
        public static void Run()
        {
            if (Menus.LaneClearMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
            {
                if (Globals.IsStunReady)
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

            if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (Globals.CanUseSpell(SpellSlot.W))
                {
                    Vector3 _PredictionW = new Vector3();
                    if (Globals.GetBestLocationW(GameObjectType.obj_AI_Minion, out _PredictionW) >= Menus.LaneClearMenu.Get<MenuSlider>("MinMinions").CurrentValue)
                    {
                        if (_PredictionW != Vector3.Zero)
                        {
                            Globals.DelayAction(() => SpellsManager.W.Cast(_PredictionW));
                        }
                    }
                }
            }
        }
    }
}
