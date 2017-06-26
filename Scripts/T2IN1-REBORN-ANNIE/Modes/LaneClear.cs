using T2IN1_REBORN_ANNIE.Managers;
using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_ANNIE.Modes
{
    internal class LaneClear
    {
        public static void Run()
        {
            if (Menus.LaneClearMenu.Get<MenuCheckbox>("StopIfPassiveIsCharged").Checked)
            {
                if (Globals.IsPassiveReady) return;
            }

            if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseQ").Checked)
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

            if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (!SpellsManager.W.IsUsable()) return;

                Vector3 predictionW = new Vector3();

                if (Other.Prediction.GetBestLocationW(out predictionW) < Menus.LaneClearMenu.Get<MenuSlider>("MinMinions").CurrentValue) return;

                if (predictionW != Vector3.Zero)
                {
                    SpellsManager.W.Cast(predictionW);
                }
            }
        }
    }
}
