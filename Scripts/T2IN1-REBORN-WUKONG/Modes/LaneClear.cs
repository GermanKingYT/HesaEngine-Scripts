using System.Collections.Generic;
using System.Linq;

using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class LaneClear
    {
        public static void Run()
        {
            IEnumerable<Obj_AI_Minion> minions = Globals.GetLaneMinions(SpellsManager.E.Range);

            if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseW").Checked)
            {
                if (SpellsManager.W.IsUsable()) 
                {
                    if (minions.FirstOrDefault(x => x.CountEnemyLaneMinions(175) >= Menus.LaneClearMenu.Get<MenuSlider>("MinMinionsW").CurrentValue).IsValidTarget(SpellsManager.E.Range)) 
                    {
                        SpellsManager.W.Cast();
                    }
                }
            }

            if (Menus.LaneClearMenu.Get<MenuCheckbox>("UseE").Checked)
            {
                if (SpellsManager.E.IsUsable()) 
                {
                    Obj_AI_Base minion = minions.FirstOrDefault(x => x.CountEnemyLaneMinions(187) >= Menus.LaneClearMenu.Get<MenuSlider>("MinMinionsE").CurrentValue);
                    if (minion.IsValidTarget(SpellsManager.E.Range)) 
                    {
                        SpellsManager.E.Cast(minion);
                    }
                }
            }  
        }
    }
}
