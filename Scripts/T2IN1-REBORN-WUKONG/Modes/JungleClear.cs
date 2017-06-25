using System.Collections.Generic;
using System.Linq;

using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class JungleClear
    {
        public static void Run()
        {
            IEnumerable<Obj_AI_Minion> jungleMinions = SpellsManager.E.GetJungleMinions();

            if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseW").Checked && SpellsManager.W.IsUsable())
            {
                if (jungleMinions.FirstOrDefault().IsValidTarget(SpellsManager.W.Range)) 
                {
                    SpellsManager.W.Cast();
                }
            }

            if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseE").Checked && SpellsManager.E.IsUsable()) 
            {
                if (jungleMinions.FirstOrDefault().IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.E.Cast(jungleMinions.FirstOrDefault());
                }
            }
        }
    }
}
