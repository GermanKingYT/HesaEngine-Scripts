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
        public static bool IsUsable(this Spell spell) => ObjectManager.Me.Spellbook.GetSpellState(spell.Slot).Equals(SpellState.Ready);
        public static IEnumerable<Spell> UsableSpells => new[] { new Spell(SpellSlot.Q), new Spell(SpellSlot.W), new Spell(SpellSlot.E), new Spell(SpellSlot.R) }.Where(x => x.IsUsable());

        public static float GetSmallestRange(this List<Spell> spells) => spells.OrderBy(s => s.Range).FirstOrDefault()?.Range ?? 0f;
        public static float GetHighestRange(this List<Spell> spells) => spells.OrderByDescending(s => s.Range).FirstOrDefault()?.Range ?? 0f;

        public static int CollisionCount(this Spell spell, Obj_AI_Base entity) => spell.GetPrediction(entity).CollisionObjects.Count;

        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius, float speed, CollisionableObjects[] collisionObjects) => entity == null ? null : new Prediction().GetPrediction(entity, delay, radius, speed, collisionObjects);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius, float speed) => entity == null ? null : new Prediction().GetPrediction(entity, delay, radius, speed);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius) => entity == null ? null : new Prediction().GetPrediction(entity, delay, radius);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay) => entity == null ? null : new Prediction().GetPrediction(entity, delay);

        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius, float speed, CollisionableObjects[] collisionObjects) {  if (entity == null) return; PredictionOutput _Prediction = GetMyPrediction(entity, delay, radius, speed, collisionObjects); if (_Prediction.Hitchance >= hitchance) { spell.Cast(_Prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius, float speed) { if (entity == null) return; PredictionOutput _Prediction = GetMyPrediction(entity, delay, radius, speed); if (_Prediction.Hitchance >= hitchance)  { spell.Cast(_Prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay, float radius) { if (entity == null) return; PredictionOutput _Prediction = GetMyPrediction(entity, delay, radius); if (_Prediction.Hitchance >= hitchance)  { spell.Cast(_Prediction.CastPosition); } }
        public static void CastWithPrediction(this Spell spell, Obj_AI_Base entity, HitChance hitchance, float delay) { if (entity == null) return; PredictionOutput _Prediction = GetMyPrediction(entity, delay); if (_Prediction.Hitchance >= hitchance)  { spell.Cast(_Prediction.CastPosition); } }

        public static bool CanCastSpell(this Obj_AI_Base entity, Spell spell) => entity != null && spell != null && entity.IsValidTarget(spell.Range) && spell.IsUsable();
        public static bool CanCastSpell(this Vector3 entity, Spell spell) => entity != null && spell != null && entity.IsInRange(ObjectManager.Me.Position, spell.Range) && spell.IsUsable();
        public static bool CanCastSpell(this Obj_AI_Base entity, Spell spell, HitChance hitchance) => entity != null && spell != null && entity.CanCastSpell(spell) && spell.GetPrediction(entity).Hitchance >= hitchance;
    }
}
