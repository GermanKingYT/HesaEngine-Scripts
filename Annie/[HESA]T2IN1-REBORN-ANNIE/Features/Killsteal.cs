using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

using _HESA_T2IN1_REBORN_ANNIE.Managers;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal class Killsteal
    {
        public static void Run()
        {
            if (SpellSlot.Q.CanUseSpell() && SpellSlot.W.CanUseSpell())
            {
                var _Slots = new[] { SpellSlot.Q, SpellSlot.W };
                var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(625) && Globals.MyHero.GetComboDamage(e, _Slots) >= e.Health);
                if (_Target.IsTargetValidWithRange(625))
                {
                    Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                    Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                }
            }
            else if (SpellSlot.Q.CanUseSpell())
            {
                var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.Q.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.Q) >= e.Health);
                if (_Target.IsTargetValidWithRange(SpellsManager.Q.Range))
                {
                    Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                }
            }
            else if (SpellSlot.W.CanUseSpell())
            {
                var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.W.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.W) >= e.Health);
                if (_Target.IsTargetValidWithRange(SpellsManager.W.Range))
                {
                    Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                }
            }
        }
    }
}
