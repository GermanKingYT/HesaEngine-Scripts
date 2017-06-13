using System;
using System.Collections.Generic;
using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE.Other
{
    internal class Prediction
    {
        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        private static List<Vector3> GetEnemiePositions()
        {
            return ObjectManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsVisible && e.IsValidTarget() && Globals.MyHero.Distance(e) <= 1200).Select(Hero => new HesaEngine.SDK.Prediction().GetPrediction(Hero, 500).UnitPosition).ToList();
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        private static int CountUltimateHits(Vector2 CastPosition)
        {
            return GetEnemiePositions().To2D().Count(EnemyPos => CastPosition.Distance(EnemyPos) <= 250);
        }

        /* Pasta from WuAnnie */ /* TODO: Rework later, needs improvement */
        public static Dictionary<Vector2, int> GetBestUltimatePosition(Vector2 TargetPosition)
        {
            List<Vector2> _UltimatePosition = new List<Vector2>
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

            Dictionary<Vector2, int> _PositionAndHits = _UltimatePosition.ToDictionary(_Position => _Position, CountUltimateHits);
            Vector2 _PosToGG = _PositionAndHits.First(pos => pos.Value == _PositionAndHits.Values.Max()).Key;
            int _Hits = _PositionAndHits.First(pos => pos.Key == _PosToGG).Value;
            return new Dictionary<Vector2, int> { { _PosToGG, _Hits } };
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        public static int GetBestLocationW(GameObjectType type, out Vector3 pos)
        {
            List<Geometry.Polygon.Sector> _SectorList = new List<Geometry.Polygon.Sector>();
            pos = Vector3.Zero;

            List<Obj_AI_Minion> _MinionList = ObjectManager.MinionsAndMonsters.Enemy.Where(m => !m.IsDead && m.IsValidTarget(SpellsManager.W.Range)).OrderByDescending(m => m.Distance(Globals.MyHero)).ToList();
            List<AIHeroClient> _ChampionList = ObjectManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsValidTarget(SpellsManager.W.Range)).OrderByDescending(e => e.Distance(Globals.MyHero)).ToList();

            Obj_AI_Base _Enemy = type == GameObjectType.AIHeroClient ? _ChampionList.FirstOrDefault() : (Obj_AI_Base)_MinionList.FirstOrDefault();

            if (_Enemy == null)
                return 0;

            List<Vector3> _Vectors = new List<Vector3>
            {
                new Vector3(_Enemy.ServerPosition.X + 550, _Enemy.ServerPosition.Y, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X - 550, _Enemy.ServerPosition.Y, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X, _Enemy.ServerPosition.Y + 550, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X, _Enemy.ServerPosition.Y - 550, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X + 230, _Enemy.ServerPosition.Y, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X - 230, _Enemy.ServerPosition.Y, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X, _Enemy.ServerPosition.Y + 230, _Enemy.ServerPosition.Z),
                new Vector3(_Enemy.ServerPosition.X, _Enemy.ServerPosition.Y - 230, _Enemy.ServerPosition.Z),
                _Enemy.ServerPosition
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

            _SectorList.Add(_Sector1);
            _SectorList.Add(_Sector2);
            _SectorList.Add(_Sector3);
            _SectorList.Add(_Sector4);
            _SectorList.Add(_Sector5);
            _SectorList.Add(_Sector6);
            _SectorList.Add(_Sector7);
            _SectorList.Add(_Sector8);
            _SectorList.Add(_Sector9);

            List<int> _CSHits = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int _Count = 0; _Count < 9; _Count++)
            {
                if (type == GameObjectType.AIHeroClient)
                {
                    foreach (Obj_AI_Base _Champion in _ChampionList)
                    {
                        if (_SectorList.ElementAt(_Count).IsInside(_Champion))
                        {
                            _CSHits[_Count]++;
                        }
                    }
                }
                else
                {
                    foreach (Obj_AI_Base _Minion in _MinionList)
                    {
                        if (_SectorList.ElementAt(_Count).IsInside(_Minion))
                        {
                            _CSHits[_Count]++;
                        }
                    }
                }
            }

            int _Value = _CSHits.Select((value, index) => new { Value = value, Index = index }).Aggregate((a, b) => a.Value > b.Value ? a : b).Index;
            pos = _Vectors[_Value];
            return _CSHits[_Value];
        }
    }
}
