using System.Linq;

using T2IN1_REBORN_LIB.Helpers;

using T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_ANNIE.Features
{
    internal class Killsteal
    {
        public static void Run()
        {
            if (SpellsManager.Q.IsUsable() && SpellsManager.W.IsUsable())
            {
                AIHeroClient target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(625) && Globals.MyHero.GetComboDamage(e, new[] { SpellSlot.Q, SpellSlot.W }) >= e.Health);

                if (!target.IsValidTarget(625)) return;

                SpellsManager.Q.Cast(target);
                SpellsManager.W.CastOnUnit(target);
            }
            else if (SpellsManager.Q.IsUsable())
            {
                AIHeroClient target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.Q.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.Q) >= e.Health);

                if (target.IsValidTarget(SpellsManager.Q.Range))
                {
                    SpellsManager.Q.Cast(target);
                }
            }
            else if (SpellsManager.W.IsUsable())
            {
                AIHeroClient target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.W.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.W) >= e.Health);

                if (target.IsValidTarget(SpellsManager.W.Range))
                {
                    SpellsManager.W.CastOnUnit(target);
                }
            }
        }
    }
}
