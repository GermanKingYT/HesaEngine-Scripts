using System;
using System.Collections.Generic;
using System.Linq;

using T2IN1_REBORN_LIB;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using T2IN1_REBORN_LIB.Helpers;

namespace T2IN1_REBORN_WUKONG
{
    internal static class Globals
    {
        public static AIHeroClient MyHero => ObjectManager.Me;
        public static float MyHeroManaPercent => MyHero.ManaPercent;

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;
        public static Orbwalker.OrbwalkingMode OrbwalkerMode => Orb.ActiveMode;

        public static IEnumerable<Obj_AI_Base> CachedEnemies { get; set; }
        public static IEnumerable<Obj_AI_Base> CachedAllies { get; set; }

        public static bool IsUltimateActive => MyHero.HasBuff("MonkeyKingSpinToWin");

        private static float MyMinionHealthPrediction(this Obj_AI_Base minion, Spell daSpell) => MinionHealthPrediction.GetHealthPrediction(minion, Game.GameTimeTickCount, (int)Math.Ceiling(daSpell.Delay));
        private static Obj_AI_Base LastMinion { get; set; }
        public static Obj_AI_Base GetLaneMinion(Spell daSpell)
        {
            IEnumerable<Obj_AI_Minion> minions = daSpell.GetLaneMinions();

            if (minions.IsEmpty())
                return null;

            Obj_AI_Minion temp = minions.FirstOrDefault();

            if (temp.Equals(LastMinion))
            {
                if (!(minions.Count() > 1))
                    return null;

                temp = minions.ElementAt(1);
            }

            if (!temp.IsValidTarget())
                return null;

            if (MyHero.CanAttack && MyHero.GetAutoAttackDamage(temp) >= MyMinionHealthPrediction(temp, daSpell))
                return null;

            if (!(daSpell.GetDamage(temp) >= MyMinionHealthPrediction(temp, daSpell)))
                return null;

            LastMinion = temp; return temp;
        }
    }
}
