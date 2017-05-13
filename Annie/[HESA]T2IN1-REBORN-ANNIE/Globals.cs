using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public static Orbwalker.OrbwalkerInstance Orb { get; set; }
        public static Orbwalker.OrbwalkingMode OrbwalkerMode => Orb.ActiveMode;

        public static bool IsTargetValid(this AIHeroClient target) => target != null && target.IsValidTarget();
        public static bool IsTargetValidWithRange(this AIHeroClient target, float range) => target != null && target.IsValidTarget(range);
        public static bool IsObjectValid(this Obj_AI_Base target) => target != null && target.IsValidTarget();
        public static bool IsObjectValidWithRange(this Obj_AI_Base target, float range) => target != null && target.IsValidTarget(range);

        public static Obj_AI_Minion Tibbers { get; set; }
        public static bool IsTibbersSpawned { get; set; }
        public static bool IsStunReady => MyHero.HasBuff("pyromania_particle");

        private static Obj_AI_Base _LastMinion { get; set; }
        public static Obj_AI_Base GetLaneMinion(Spell daSpell)
        {
            var _Minions = MinionManager.GetMinions(daSpell.Range).OrderBy(x => x.Health);
            if (!_Minions.IsEmpty())
            {
                var _Temp = _Minions.FirstOrDefault();
                if (_Temp != null && _Temp.IsValidTarget(daSpell.Range) && !_Temp.IsDead)
                {
                    if (_Temp.Equals(_LastMinion))
                    {
                        if (_Minions.Count() > 1) { _Temp = _Minions.Skip(1).First(); } else { return null; }
                    }

                    if (daSpell.GetDamage(_Temp) >= MinionHealthPrediction.GetHealthPrediction(_Temp, Game.GameTimeTickCount, (int)Math.Ceiling(daSpell.Delay)))
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

        public static Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
            return tcs.Task;
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
