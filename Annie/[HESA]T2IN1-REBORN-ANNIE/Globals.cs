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

        private static float _MinionHealthPrediction(this Obj_AI_Base minion, Spell daSpell) => MinionHealthPrediction.GetHealthPrediction(minion, Game.GameTimeTickCount, (int) Math.Ceiling(daSpell.Delay));
        private static Obj_AI_Base _LastMinion { get; set; }
        public static Obj_AI_Base GetLaneMinion(Spell daSpell)
        {
            var _Minions = MinionManager.GetMinions(daSpell.Range).Where(x => x.IsValidTarget() && !x.IsDead).OrderBy(x => x.Health);
            if (!_Minions.IsEmpty())
            {
                var _Temp = _Minions.FirstOrDefault();
                if (_Temp.Equals(_LastMinion))
                {
                    if (_Minions.Count() > 1) { _Temp = _Minions.Skip(1).First(); } else { return null; }
                }

                if (_Temp.IsValidTarget())
                {
                    if (MyHero.CanAttack && MyHero.GetAutoAttackDamage(_Temp) >= _MinionHealthPrediction(_Temp, daSpell))
                    {
                        _Temp = _Minions.Skip(1).FirstOrDefault();
                        if (_Temp.IsValidTarget())
                        {
                            if (daSpell.GetDamage(_Temp) >= _MinionHealthPrediction(_Temp, daSpell))
                            {
                                _LastMinion = _Temp; return _Temp;
                            }
                        } 
                    }
                    else if (daSpell.GetDamage(_Temp) >= _MinionHealthPrediction(_Temp, daSpell))
                    {
                        _LastMinion = _Temp; return _Temp;
                    }
                }
            }
            return null;
        }

        public static void DelayAction(Action action)
        {
            Core.DelayAction(action, Randomizer.Next(300, 500));
        }

        public static int RandomDigits(int length)
        {
            int _Number = 0;
            for (int i = 0; i < length; i++)
            {
                _Number = Randomizer.Next(10);
            }
            return _Number;
        }
    }
}
