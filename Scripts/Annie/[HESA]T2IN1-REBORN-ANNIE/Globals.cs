using System;
using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE
{
    internal static class Globals
    {
        public static Random Randomizer = new Random();

        public static float MyHeroManaPercent => MyHero.ManaPercent;
        public static AIHeroClient MyHero => ObjectManager.Me;

        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();

        public static bool CanUseSpell(this SpellSlot spell) => MyHero.Spellbook.GetSpellState(spell) == SpellState.Ready;
        public static bool AllSpellsReady => SpellSlot.Q.CanUseSpell() && SpellSlot.W.CanUseSpell() && SpellSlot.R.CanUseSpell();

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;
        public static Orbwalker.OrbwalkingMode OrbwalkerMode => Orb.ActiveMode;

        public static Obj_AI_Minion Tibbers => ObjectManager.MinionsAndMonsters.Ally.FirstOrDefault(x => x.Name.ToLower().Contains("tibbers"));
        public static bool IsTibbersSpawned => MyHero.GetSpell(SpellSlot.R).SpellData.Name.Equals("InfernalGuardianGuide");
        public static bool IsStunReady => MyHero.HasBuff("pyromania_particle");

        private static float MyMinionHealthPrediction(this Obj_AI_Base minion, Spell daSpell) => MinionHealthPrediction.GetHealthPrediction(minion, Game.GameTimeTickCount, (int)Math.Ceiling(daSpell.Delay));
        private static Obj_AI_Base LastMinion { get; set; }
        public static Obj_AI_Base GetLaneMinion(Spell daSpell)
        {
            List<Obj_AI_Minion> _Minions = MinionManager.GetMinions(daSpell.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);
            if (_Minions.IsEmpty())
                return null;

            Obj_AI_Minion _Temp = _Minions.FirstOrDefault();
            if (_Temp.Equals(LastMinion))
            {
                if (!(_Minions.Count() > 1))
                    return null;

                _Temp = _Minions.ElementAt(1);
            }

            if (!_Temp.IsValidTarget())
                return null;

            if (MyHero.CanAttack && MyHero.GetAutoAttackDamage(_Temp) >= MyMinionHealthPrediction(_Temp, daSpell))
                return null;

            if (!(daSpell.GetDamage(_Temp) >= MyMinionHealthPrediction(_Temp, daSpell)))
                return null;

            LastMinion = _Temp; return _Temp;
        }

        public static void DelayAction(Action action)
        {
            Core.DelayAction(action, Randomizer.Next(300, 500));
        }
    }
}
