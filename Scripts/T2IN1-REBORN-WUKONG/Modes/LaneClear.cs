using System;
using System.Linq;

using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class LaneClear
    {
        public static void Run()
        {
            if (SpellsManager.Q.IsUsable())
            {
                Obj_AI_Base minion = Entities.GetLaneMinions(300).FirstOrDefault(x => SpellsManager.Q.GetDamage(x) >= MinionHealthPrediction.GetHealthPrediction(x, Game.GameTimeTickCount + 100, (int)Math.Ceiling(SpellsManager.Q.Delay)));
                if (minion.IsValidTarget(SpellsManager.Q.Range))
                {
                    SpellsManager.Q.Cast();
                }
            }

            if (SpellsManager.W.IsUsable()) 
            {
                Obj_AI_Base minion = Entities.GetLaneMinions(125).FirstOrDefault();
                if (minion.IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.W.Cast();
                }
            }

            if (SpellsManager.E.IsUsable()) 
            {
                Obj_AI_Base minion = Entities.GetLaneMinions(625).FirstOrDefault(x => SpellsManager.E.GetDamage(x) >= MinionHealthPrediction.GetHealthPrediction(x, Game.GameTimeTickCount, (int)Math.Ceiling(SpellsManager.E.Delay)));
                if (minion.IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.E.Cast(minion);
                }
            }
        }
    }
}
