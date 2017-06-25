using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class Combo
    {
        public static void Run()
        {
            AIHeroClient targetE = TargetSelector.GetTarget(SpellsManager.E.Range);
            AIHeroClient targetR = TargetSelector.GetTarget(SpellsManager.R.Range);

            if (Globals.IsUltimateActive) return;

            if (SpellsManager.E.IsUsable() && targetE.IsValidTarget(SpellsManager.E.Range)) 
            {
                SpellsManager.E.Cast(targetE);
            }

            if (SpellsManager.R.IsUsable() && targetR.IsValidTarget(SpellsManager.R.Range)) 
            {
                SpellsManager.R.Cast();
            }
        }
    }
}
