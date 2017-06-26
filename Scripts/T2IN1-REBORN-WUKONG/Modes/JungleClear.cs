using System.Collections.Generic;
using System.Linq;

using T2IN1_REBORN_WUKONG.Managers;
using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Modes
{
    internal class JungleClear
    {
        public static void Run()
        {
            IEnumerable<Obj_AI_Minion> jungleMonsters = SpellsManager.E.GetJungleMinions();

            if (jungleMonsters == null || !jungleMonsters.Any()) return;

            if (jungleMonsters.FirstOrDefault().IsValidTarget(Globals.MyHero.GetRealAutoAttackRange()) && jungleMonsters.FirstOrDefault().Health <= Globals.MyHero.GetAutoAttackDamage(jungleMonsters.FirstOrDefault())) return;
            if (jungleMonsters.FirstOrDefault().IsValidTarget(300) && SpellsManager.Q.IsUsable() && jungleMonsters.FirstOrDefault().Health <= Globals.MyHero.GetSpellDamage(jungleMonsters.FirstOrDefault(), SpellSlot.Q) + Globals.MyHero.GetAutoAttackDamage(jungleMonsters.FirstOrDefault())) return;

            Obj_AI_Minion bigJungleMonster = jungleMonsters.FirstOrDefault(bigMonster => !bigMonster.Name.ToLower().Equals("sru_crab") && Globals.SmiteMobs.Any(smiteMob => bigMonster.Name.ToLower().Equals(smiteMob.ToLower())));

            if (bigJungleMonster != null && bigJungleMonster.IsValidTarget(Globals.MyHero.GetRealAutoAttackRange()) && bigJungleMonster.Health <= Globals.MyHero.GetAutoAttackDamage(bigJungleMonster)) return;
            if (bigJungleMonster != null && bigJungleMonster.IsValidTarget(300) && SpellsManager.Q.IsUsable() && bigJungleMonster.Health <= Globals.MyHero.GetSpellDamage(bigJungleMonster, SpellSlot.Q) + Globals.MyHero.GetAutoAttackDamage(bigJungleMonster)) return;

            if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseW").Checked && SpellsManager.W.IsUsable())
            {
                if (bigJungleMonster != null && bigJungleMonster.IsValidTarget(SpellsManager.W.Range))
                {
                    SpellsManager.W.Cast();
                }
                else if (jungleMonsters.Count(x => x.IsValidTarget(SpellsManager.W.Range)) >= 1)
                {
                    SpellsManager.W.Cast();
                }
            }

            if (Menus.JungleClearMenu.Get<MenuCheckbox>("UseE").Checked && SpellsManager.E.IsUsable()) 
            {
                if (bigJungleMonster != null && bigJungleMonster.IsValidTarget(SpellsManager.E.Range)) 
                {
                    SpellsManager.E.Cast(bigJungleMonster);
                }
                else if (jungleMonsters.Count(x => x.IsValidTarget(SpellsManager.E.Range)) >= 1) 
                {
                    SpellsManager.E.Cast(jungleMonsters.FirstOrDefault());
                }
            }
        }
    }
}
