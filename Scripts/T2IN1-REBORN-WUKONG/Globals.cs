﻿using System;
using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG
{
    internal static class Globals
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();

        public static AIHeroClient MyHero => ObjectManager.Me;
        public static float MyHeroManaPercent => MyHero.ManaPercent;

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;
        public static Orbwalker.OrbwalkingMode OrbwalkerMode => Orb.ActiveMode;

        public static IEnumerable<Obj_AI_Base> CachedEnemies { get; set; }

        public static bool IsUsable(this Spell spell) => ObjectManager.Me.Spellbook.GetSpellState(spell.Slot).Equals(SpellState.Ready);

        public static double TotalSpellDamage(Obj_AI_Base target)
        {
            if (target == null || !target.IsValid()) return 0;

            SpellSlot[] slots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            double damage = ObjectManager.Me.Spellbook.Spells.Where(s => s.Slot.IsReady() && slots.Contains(s.Slot)).Sum(s => ObjectManager.Me.GetSpellDamage(target, s.Slot));
            double autoAttackDamage = Orbwalker.CanAttack() ? ObjectManager.Me.GetAutoAttackDamage(target) : 0f;
            return damage + autoAttackDamage;
        }

        public static bool IsUltimateActive => MyHero.HasBuff("monkeykingspintowin");

        public static readonly string[] SmiteMobs =
        {
            "SRU_Red", "SRU_Blue", "SRU_Dragon_Water", "SRU_Dragon_Fire", "SRU_Dragon_Earth", "SRU_Dragon_Air",
            "SRU_Dragon_Elder", "SRU_Baron", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak", "SRU_RiftHerald", "SRU_Krug",
            "Sru_Crab", "TT_Spiderboss", "TT_NGolem", "TT_NWolf", "TT_NWraith"
        };

        public static IEnumerable<Obj_AI_Base> GetEnemies => ObjectManager.Heroes.Enemies.Where(x => x.IsValid());
        public static Obj_AI_Base GetBestTarget(this Spell spell) => ObjectManager.Heroes.Enemies.OrderBy(e => e.Health).ThenByDescending(TargetSelector.GetPriority).ThenBy(e => e.FlatArmorMod).ThenBy(e => e.FlatMagicReduction).FirstOrDefault(e => e.IsValidTarget(spell.Range) && !e.HasUndyingBuff());

        public static IEnumerable<Obj_AI_Minion> GetJungleMinions(this Spell spell) => ObjectManager.MinionsAndMonsters.NeutralCamps.OrderByDescending(x => x.Health).Where(x => x.IsValidTarget(spell.Range));

        public static IEnumerable<Obj_AI_Minion> GetLaneMinions(this Spell spell) => MinionManager.GetMinions(spell.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);
        public static IEnumerable<Obj_AI_Minion> GetLaneMinions(float range) => MinionManager.GetMinions(range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);
        public static int CountEnemyLaneMinions(this Obj_AI_Base entity, float range = 100) => entity == null ? 0 : MinionManager.GetMinions(range, MinionTypes.All, MinionTeam.Enemy).Count(x => x.IsValid() && x.Distance(entity) <= range);

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
