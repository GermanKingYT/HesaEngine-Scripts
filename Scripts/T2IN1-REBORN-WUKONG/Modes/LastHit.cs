using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class LastHit
    {
        public static void Run()
        {
            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseW").Checked) 
            {
                if (SpellsManager.W.IsUsable()) 
                {
                    Obj_AI_Base minion = Globals.GetLaneMinion(SpellsManager.Q);
                    if (minion.IsValidTarget(SpellsManager.E.Range)) 
                    {
                        SpellsManager.W.Cast();
                    }
                }
            }

            if (Menus.LastHitMenu.Get<MenuCheckbox>("UseE").Checked)
            {
                if (SpellsManager.E.IsUsable()) 
                {
                    Obj_AI_Base minion = Globals.GetLaneMinion(SpellsManager.Q);
                    if (minion.IsValidTarget(SpellsManager.E.Range)) 
                    {
                        SpellsManager.E.Cast(minion);
                    }
                }
            }
        }
    }
}
