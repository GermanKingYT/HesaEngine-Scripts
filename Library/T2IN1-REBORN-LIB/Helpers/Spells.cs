using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_LIB.Helpers
{
    public static class Spells
    {
        public static Spell Q = new Spell(SpellSlot.Q);
        public static Spell W = new Spell(SpellSlot.W);
        public static Spell E = new Spell(SpellSlot.E);
        public static Spell R = new Spell(SpellSlot.R);

        public static bool IsUsable(this Spell spell) => ObjectManager.Me.Spellbook.GetSpellState(spell.Slot).Equals(SpellState.Ready);
        public static IEnumerable<Spell> UsableSpells => new[] { Q, W, E, R }.Where(x => x.IsUsable());

        public static float GetSmallestRange(this List<Spell> spells) => spells.OrderBy(s => s.Range).FirstOrDefault()?.Range ?? 0f;
        public static float GetHighestRange(this List<Spell> spells) => spells.OrderByDescending(s => s.Range).FirstOrDefault()?.Range ?? 0f;

        public static int CollisionCount(this Spell spell, Obj_AI_Base entity) => spell.GetPrediction(entity).CollisionObjects.Count;

        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius, float speed, CollisionableObjects[] collisionObjects) { PredictionOutput prediction = entity?.GetMyPrediction(delay, radius, speed, collisionObjects); if (prediction?.Hitchance >= hitchance) { spell.Cast(prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius, float speed) { PredictionOutput prediction = entity?.GetMyPrediction(delay, radius, speed); if (prediction?.Hitchance >= hitchance)  { spell.Cast(prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius) { PredictionOutput prediction = entity?.GetMyPrediction(delay, radius); if (prediction?.Hitchance >= hitchance)  { spell.Cast(prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay) { PredictionOutput prediction = entity?.GetMyPrediction(delay); if (prediction?.Hitchance >= hitchance)  { spell.Cast(prediction.CastPosition); } }

        public static bool CanCastSpell(this Obj_AI_Base entity, Spell spell) => entity != null && spell != null && entity.IsValidTarget(spell.Range) && spell.IsUsable();
        public static bool CanCastSpell(this Vector3 entity, Spell spell) => entity != null && spell != null && entity.IsInRange(ObjectManager.Me.Position, spell.Range) && spell.IsUsable();
        public static bool CanCastSpell(this Obj_AI_Base entity, Spell spell, HitChance hitchance) => entity != null && spell != null && entity.CanCastSpell(spell) && spell.GetPrediction(entity).Hitchance >= hitchance;

        public static double TotalSpellDamage(Obj_AI_Base target)
        {
            if (target == null || !target.IsValid()) return 0;

            SpellSlot[] slots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            double damage = ObjectManager.Me.Spellbook.Spells.Where(s => s.Slot.IsReady() && slots.Contains(s.Slot)).Sum(s => ObjectManager.Me.GetSpellDamage(target, s.Slot));
            double autoAttackDamage = Orbwalker.CanAttack() ? ObjectManager.Me.GetAutoAttackDamage(target) : 0f;
            return damage + autoAttackDamage;
        }
    }
}
