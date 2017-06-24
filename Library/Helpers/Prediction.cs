using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_LIB.Helpers
{
    public static class Prediction
    {
        public static Vector3 GetBestCastPosition(GameObjectType type, Spell spell, int minHits, int spellRadius)
        {
            IEnumerable<Obj_AI_Base> entities = type.Equals(GameObjectType.AIHeroClient) ? Entities.GetEnemies.Where(x => x.IsValidTarget(spell.Range)) : MinionManager.GetMinions(spell.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);

            if (!entities.Any()) return Vector3.Zero;

            List<Obj_AI_Base> bestEnemies = new List<Obj_AI_Base>();

            foreach (Obj_AI_Base entity in entities) 
            {
                int hitCount = 0;
                foreach (Obj_AI_Base entity2 in entities) 
                {
                    if (entity.Position.Distance(entity2.Position) <= 2 * spellRadius) 
                    {
                        hitCount++;
                    }

                    if (hitCount >= minHits) 
                    {
                        bestEnemies.Add(entity);
                    }
                }
            }

            if (bestEnemies.Count < minHits || !bestEnemies.Any()) return Vector3.Zero;

            float Xs = 0;
            float Zs = 0;
            foreach (Obj_AI_Base enemy in bestEnemies) 
            {
                Xs += enemy.Position.X;
                Zs += enemy.Position.Z;
            }

            float avgX = Xs / bestEnemies.Count;
            float avgZ = Zs / bestEnemies.Count;

            Vector2 bestPosition = new Vector2(avgX, avgZ);

            return bestPosition.Distance(ObjectManager.Me.Position.To2D()) <= spell.Range ? bestPosition.To3D() : Vector3.Zero;
        }
    }
}
