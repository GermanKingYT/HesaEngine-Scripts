using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal class Killsteal
    {
        public static void Run()
        {
            if (SpellSlot.Q.CanUseSpell() && SpellSlot.W.CanUseSpell())
            {
                SpellSlot[] _Slots = new[] { SpellSlot.Q, SpellSlot.W };
                AIHeroClient _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(625) && Globals.MyHero.GetComboDamage(e, _Slots) >= e.Health);
                if (_Target.IsValidTarget(625))
                {
                    Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                    Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                }
            }
            else if (SpellSlot.Q.CanUseSpell())
            {
                AIHeroClient _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.Q.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.Q) >= e.Health);
                if (_Target.IsValidTarget(SpellsManager.Q.Range))
                {
                    Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                }
            }
            else if (SpellSlot.W.CanUseSpell())
            {
                AIHeroClient _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.W.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.W) >= e.Health);
                if (_Target.IsValidTarget(SpellsManager.W.Range))
                {
                    Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                }
            }
        }
    }
}
