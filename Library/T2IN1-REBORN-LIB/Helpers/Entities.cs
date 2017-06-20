using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Data;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_LIB.Helpers
{
    public static class Entities
    {
        public static bool IsNull(this Obj_AI_Base entity) => entity == null;

        public static bool HasBuff(Obj_AI_Base entity, string buffName) => entity.HasBuff(buffName);
        public static List<BuffInstance> HasBuffs(Obj_AI_Base entity) => entity.Buffs;

        public static bool CanMove(this Obj_AI_Base entity)
        {
            return !(entity.MovementSpeed < 50)
                   && !entity.IsStunned
                   && !entity.HasBuffOfType(BuffType.Stun)
                   && !entity.HasBuffOfType(BuffType.Fear)
                   && !entity.HasBuffOfType(BuffType.Snare)
                   && !entity.HasBuffOfType(BuffType.Knockup)
                   && !entity.HasBuff("Recall")
                   && !entity.HasBuffOfType(BuffType.Knockback)
                   && !entity.HasBuffOfType(BuffType.Charm)
                   && !entity.HasBuffOfType(BuffType.Taunt)
                   && !entity.HasBuffOfType(BuffType.Suppression)
                   && (!entity.Spellbook.IsChanneling
                       || entity.IsMoving);
        }

        public static Obj_AI_Base GetJungleMinion(this Spell spell) => ObjectManager.MinionsAndMonsters.NeutralCamps.OrderByDescending(x => x.Health).FirstOrDefault(x => x.IsValidTarget(spell.Range));
        public static int CountJungleMinions(this Obj_AI_Base entity, float range = 100) => ObjectManager.MinionsAndMonsters.NeutralCamps.Count(x => x.IsValid() && x.Distance(entity) <= range);
        public static int CountEnemyLaneMinions(this Obj_AI_Base entity, float range = 100) => MinionManager.GetMinions(range, MinionTypes.All, MinionTeam.Enemy).Count(x => x.IsValid() && x.Distance(entity) <= range);
        public static int CountAllyLaneMinions(this Obj_AI_Base entity, float range = 100) => MinionManager.GetMinions(range, MinionTypes.All, MinionTeam.Ally).Count(x => x.IsValid() && x.Distance(entity) <= range);

        public static IEnumerable<Obj_AI_Base> GetEnemysInRange(float range) => ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(range));
        public static Obj_AI_Base GetLowestEnemy => ObjectManager.Heroes.Enemies.Where(x => x.IsValid() && !x.HasUndyingBuff()).MinOrDefault(x => x.Health);
        public static Obj_AI_Base GetBestTarget(this Spell spell) => ObjectManager.Heroes.Enemies.OrderBy(e => e.Health).ThenByDescending(TargetSelector.GetPriority).ThenBy(e => e.FlatArmorMod).ThenBy(e => e.FlatMagicReduction).FirstOrDefault(e => e.IsValidTarget(spell.Range) && !e.HasUndyingBuff());

        public static Obj_AI_Base GetNearestAlly(float range = 700) => ObjectManager.Heroes.Allies.OrderBy(a => a.Distance(ObjectManager.Me)).FirstOrDefault(ally => ally.IsInRange(ObjectManager.Me, range));
        public static Obj_AI_Base GetNearestLowestAlly(float range = 700) => ObjectManager.Heroes.Allies.OrderBy(a => a.Distance(ObjectManager.Me)).ThenBy(a => a.Health).FirstOrDefault(ally => ally.IsInRange(ObjectManager.Me, range));
    }
}
