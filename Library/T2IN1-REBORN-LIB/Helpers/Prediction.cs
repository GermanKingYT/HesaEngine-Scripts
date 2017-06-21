using System;
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
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius, float speed, CollisionableObjects[] collisionObjects) => entity == null ? null : new HesaEngine.SDK.Prediction().GetPrediction(entity, delay, radius, speed, collisionObjects);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius, float speed) => entity == null ? null : new HesaEngine.SDK.Prediction().GetPrediction(entity, delay, radius, speed);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay, float radius) => entity == null ? null : new HesaEngine.SDK.Prediction().GetPrediction(entity, delay, radius);
        public static PredictionOutput GetMyPrediction(this Obj_AI_Base entity, float delay) => entity == null ? null : new HesaEngine.SDK.Prediction().GetPrediction(entity, delay);

        public static class Annie
        {
            public static class R
            {
                private static int CountHits(Vector2 castPosition)
                {
                    return Entities.GetEnemysInRange(Spells.R.Range).Select(Hero => GetMyPrediction(Hero, 500).UnitPosition).ToList().To2D().Count(x => castPosition.Distance(x) <= 250);
                }

                public static Dictionary<Vector2, int> BestPosition(Vector2 TargetPosition)
                {
                    List<Vector2> ultimatePositions = new List<Vector2>
                    {
                        new Vector2(TargetPosition.X - 250, TargetPosition.Y + 100),
                        new Vector2(TargetPosition.X - 250, TargetPosition.Y),

                        new Vector2(TargetPosition.X - 200, TargetPosition.Y + 300),
                        new Vector2(TargetPosition.X - 200, TargetPosition.Y + 200),
                        new Vector2(TargetPosition.X - 200, TargetPosition.Y + 100),
                        new Vector2(TargetPosition.X - 200, TargetPosition.Y - 100),
                        new Vector2(TargetPosition.X - 200, TargetPosition.Y),

                        new Vector2(TargetPosition.X - 160, TargetPosition.Y - 160),

                        new Vector2(TargetPosition.X - 100, TargetPosition.Y + 300),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y + 200),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y + 100),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y + 250),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y - 200),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y - 100),
                        new Vector2(TargetPosition.X - 100, TargetPosition.Y),

                        new Vector2(TargetPosition.X, TargetPosition.Y + 300),
                        new Vector2(TargetPosition.X, TargetPosition.Y + 270),
                        new Vector2(TargetPosition.X, TargetPosition.Y + 200),
                        new Vector2(TargetPosition.X, TargetPosition.Y + 100),

                        new Vector2(TargetPosition.X, TargetPosition.Y),

                        new Vector2(TargetPosition.X, TargetPosition.Y - 100),
                        new Vector2(TargetPosition.X, TargetPosition.Y - 200),

                        new Vector2(TargetPosition.X + 100, TargetPosition.Y),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y - 100),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y - 200),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y + 100),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y + 200),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y + 250),
                        new Vector2(TargetPosition.X + 100, TargetPosition.Y + 300),

                        new Vector2(TargetPosition.X + 160, TargetPosition.Y - 160),

                        new Vector2(TargetPosition.X + 200, TargetPosition.Y),
                        new Vector2(TargetPosition.X + 200, TargetPosition.Y - 100),
                        new Vector2(TargetPosition.X + 200, TargetPosition.Y + 100),
                        new Vector2(TargetPosition.X + 200, TargetPosition.Y + 200),
                        new Vector2(TargetPosition.X + 200, TargetPosition.Y + 300),

                        new Vector2(TargetPosition.X + 250, TargetPosition.Y),
                        new Vector2(TargetPosition.X + 250, TargetPosition.Y + 100)
                    };

                    Dictionary<Vector2, int> positionAndHits = ultimatePositions.ToDictionary(x => x, CountHits);
                    Vector2 postoGG = positionAndHits.First(x => x.Value == positionAndHits.Values.Max()).Key;
                    int hits = positionAndHits.First(x => x.Key == postoGG).Value;
                    return new Dictionary<Vector2, int> { { postoGG, hits } };
                }
            }

            public static class W
            {
                public static int BestPosition(GameObjectType type, out Vector3 pos, IEnumerable<Obj_AI_Base> objectList)
                {
                    List<Geometry.Polygon.Sector> sectorList = new List<Geometry.Polygon.Sector>();
                    pos = Vector3.Zero;

                    List<Obj_AI_Minion> minionList = ObjectManager.MinionsAndMonsters.Enemy.Where(m => !m.IsDead && m.IsValidTarget(Spells.W.Range)).OrderByDescending(m => m.Distance(ObjectManager.Me)).ToList();
                    List<AIHeroClient> championList = ObjectManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsValidTarget(Spells.W.Range)).OrderByDescending(e => e.Distance(ObjectManager.Me)).ToList();

                    Obj_AI_Base entity = type == GameObjectType.AIHeroClient ? championList.FirstOrDefault() : (Obj_AI_Base)minionList.FirstOrDefault();

                    if (entity == null) return 0;

                    List<Vector3> vectors = new List<Vector3>
                    {
                        new Vector3(entity.ServerPosition.X + 550, entity.ServerPosition.Y, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X - 550, entity.ServerPosition.Y, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X, entity.ServerPosition.Y + 550, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X, entity.ServerPosition.Y - 550, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X + 230, entity.ServerPosition.Y, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X - 230, entity.ServerPosition.Y, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X, entity.ServerPosition.Y + 230, entity.ServerPosition.Z),
                        new Vector3(entity.ServerPosition.X, entity.ServerPosition.Y - 230, entity.ServerPosition.Z),
                        entity.ServerPosition
                    };

                    float angle = (float)(5 * Math.PI / 18);
                    Geometry.Polygon.Sector _Sector1 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[0], angle, 585);
                    Geometry.Polygon.Sector _Sector2 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[1], angle, 585);
                    Geometry.Polygon.Sector _Sector3 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[2], angle, 585);
                    Geometry.Polygon.Sector _Sector4 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[3], angle, 585);
                    Geometry.Polygon.Sector _Sector5 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[4], angle, 585);
                    Geometry.Polygon.Sector _Sector6 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[5], angle, 585);
                    Geometry.Polygon.Sector _Sector7 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[6], angle, 585);
                    Geometry.Polygon.Sector _Sector8 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[7], angle, 585);
                    Geometry.Polygon.Sector _Sector9 = new Geometry.Polygon.Sector(ObjectManager.Me.Position, vectors[8], angle, 585);

                    sectorList.Add(_Sector1);
                    sectorList.Add(_Sector2);
                    sectorList.Add(_Sector3);
                    sectorList.Add(_Sector4);
                    sectorList.Add(_Sector5);
                    sectorList.Add(_Sector6);
                    sectorList.Add(_Sector7);
                    sectorList.Add(_Sector8);
                    sectorList.Add(_Sector9);

                    List<int> csHits = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    for (int count = 0; count < 9; count++)
                    {
                        foreach (var listObject in objectList)
                        {
                            if (sectorList.ElementAt(count).IsInside(listObject)) 
                            {
                                csHits[count]++;
                            }
                        }
                    }

                    int value = csHits.Select((x, index) => new { value = x, Index = index }).Aggregate((a, b) => a.value > b.value ? a : b).Index;
                    pos = vectors[value];
                    return csHits[value];
                }
            }
        }
    }
}
