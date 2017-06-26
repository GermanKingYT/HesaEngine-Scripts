using System;
using System.Collections.Generic;
using System.Linq;

using T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_ANNIE.Other
{
    internal class Prediction
    {
        public static Vector3 GetBestCircularCastPosition(GameObjectType type, Spell spell, int minHits, int spellRadius)
        {
            IEnumerable<Obj_AI_Base> entities = type.Equals(GameObjectType.AIHeroClient) ? Globals.GetEnemies.Where(x => x.IsValidTarget(spell.Range)) : MinionManager.GetMinions(spell.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);

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

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        public static int GetBestLocationW(out Vector3 pos)
        {
            List<Geometry.Polygon.Sector> sectorList = new List<Geometry.Polygon.Sector>();
            pos = Vector3.Zero;
            
            List<Obj_AI_Minion> minionList = SpellsManager.W.GetLaneMinions().OrderByDescending(m => m.Distance(Globals.MyHero)).ToList();

            Obj_AI_Base enemy = minionList.FirstOrDefault();

            if (enemy == null)
                return 0;

            List<Vector3> _Vectors = new List<Vector3>
            {
                new Vector3(enemy.ServerPosition.X + 550, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X - 550, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y + 550, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y - 550, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X + 230, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X - 230, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y + 230, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y - 230, enemy.ServerPosition.Z),
                enemy.ServerPosition
            };

            float _Angle = (float)(5 * Math.PI / 18);
            Geometry.Polygon.Sector _Sector1 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[0], _Angle, 585);
            Geometry.Polygon.Sector _Sector2 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[1], _Angle, 585);
            Geometry.Polygon.Sector _Sector3 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[2], _Angle, 585);
            Geometry.Polygon.Sector _Sector4 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[3], _Angle, 585);
            Geometry.Polygon.Sector _Sector5 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[4], _Angle, 585);
            Geometry.Polygon.Sector _Sector6 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[5], _Angle, 585);
            Geometry.Polygon.Sector _Sector7 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[6], _Angle, 585);
            Geometry.Polygon.Sector _Sector8 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[7], _Angle, 585);
            Geometry.Polygon.Sector _Sector9 = new Geometry.Polygon.Sector(Globals.MyHero.Position, _Vectors[8], _Angle, 585);

            sectorList.Add(_Sector1);
            sectorList.Add(_Sector2);
            sectorList.Add(_Sector3);
            sectorList.Add(_Sector4);
            sectorList.Add(_Sector5);
            sectorList.Add(_Sector6);
            sectorList.Add(_Sector7);
            sectorList.Add(_Sector8);
            sectorList.Add(_Sector9);

            List<int> countHits = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int count = 0; count < 9; count++) 
            {
                foreach (Obj_AI_Base minion in minionList) 
                {
                    if (sectorList.ElementAt(count).IsInside(minion)) 
                    {
                        countHits[count]++;
                    }
                }
            }

            int _Value = countHits.Select((value, index) => new { Value = value, Index = index }).Aggregate((a, b) => a.Value > b.Value ? a : b).Index;
            pos = _Vectors[_Value];
            return countHits[_Value];
        }
    }
}
