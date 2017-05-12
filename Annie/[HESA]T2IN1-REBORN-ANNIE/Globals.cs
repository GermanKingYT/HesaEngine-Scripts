using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using _HESA_T2IN1_REBORN_ANNIE.Managers;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE
{
    internal static class Globals
    {
        public static Random Randomizer = new Random();

        public static Orbwalker.OrbwalkingMode OrbwalkerMode = Orb.ActiveMode;
        public static float MyHeroManaPercent = MyHero.ManaPercent;
        public static int MyHeroLevel = MyHero.Level;
        public static int MyHeroSpellTrainingsPoints = MyHero.SpellTrainingPoints;

        public static AIHeroClient MyHero => ObjectManager.Me;
        public static Orbwalker.OrbwalkerInstance Orb;

        public static Obj_AI_Minion Tibbers;
        public static bool IsTibbersSpawned;

        private static Obj_AI_Base _LastMinion;

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

        public static bool IsStunReady()
        {
            return MyHero.HasBuff("pyromania_particle");
        }

        public static void DelayAction(Action action)
        {
            Core.DelayAction(action, Randomizer.Next(300, 500));
        }

        public static bool AllSpellsReady()
        {
            return CanUseSpell(SpellSlot.Q) && CanUseSpell(SpellSlot.W) && CanUseSpell(SpellSlot.R);
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        private static List<Vector3> _GetEnemiePositions()
        {
            List<Vector3> Positions = new List<Vector3>();
            foreach (AIHeroClient Hero in ObjectManager.Heroes.Enemies.Where(hero => !hero.IsDead && hero.IsVisible && MyHero.Distance(hero) <= 1200))
            {
                Positions.Add(Prediction.GetPrediction(Hero, 500).UnitPosition);
            }
            return Positions;
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        private static int _CountUltimateHits(Vector2 CastPosition)
        {
            int Hits = new int();
            foreach (Vector2 EnemyPos in _GetEnemiePositions().To2D())
            {
                if (CastPosition.Distance(EnemyPos) <= 250) Hits += 1;
            }
            return Hits;
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        public static Dictionary<Vector2, int> GetBestUltimatePosition(Vector2 TargetPosition)
        {
            var _PositionAndHits = new Dictionary<Vector2, int>();
            var _UltimatePosition = new List<Vector2>
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

            foreach (Vector2 _Position in _UltimatePosition)
            {
                _PositionAndHits.Add(_Position, _CountUltimateHits(_Position));
            }

            Vector2 _PosToGG = _PositionAndHits.First(pos => pos.Value == _PositionAndHits.Values.Max()).Key;
            int _Hits = _PositionAndHits.First(pos => pos.Key == _PosToGG).Value;
            return new Dictionary<Vector2, int> { { _PosToGG, _Hits } };
        }

        /* Pasta from WuAnnie */ /* TODO: MAYBE REWORK LATER */
        public static int GetBestLocationW(GameObjectType type, out Vector3 pos)
        {
            var _SectorList = new List<Geometry.Polygon.Sector>();
            pos = Vector3.Zero;

            List<Obj_AI_Minion> _MinionList = ObjectManager.MinionsAndMonsters.Enemy.Where(it => !it.IsDead && it.IsValidTarget(SpellsManager.W.Range)).OrderByDescending(it => it.Distance(MyHero)).ToList();
            List<AIHeroClient> _ChampionList = ObjectManager.Heroes.Enemies.Where(it => !it.IsDead && it.IsValidTarget(SpellsManager.W.Range)).OrderByDescending(it => it.Distance(MyHero)).ToList();

            Obj_AI_Base _Enemy = type == GameObjectType.AIHeroClient ? _ChampionList.FirstOrDefault() : (Obj_AI_Base)_MinionList.FirstOrDefault();

            if (_Enemy == null)
                return 0;

            var _Vectors = new List<Vector3>
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
            var _Sector1 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[0], _Angle, 585);
            var _Sector2 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[1], _Angle, 585);
            var _Sector3 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[2], _Angle, 585);
            var _Sector4 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[3], _Angle, 585);
            var _Sector5 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[4], _Angle, 585);
            var _Sector6 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[5], _Angle, 585);
            var _Sector7 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[6], _Angle, 585);
            var _Sector8 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[7], _Angle, 585);
            var _Sector9 = new Geometry.Polygon.Sector(MyHero.Position, _Vectors[8], _Angle, 585);

            _SectorList.Add(_Sector1);
            _SectorList.Add(_Sector2);
            _SectorList.Add(_Sector3);
            _SectorList.Add(_Sector4);
            _SectorList.Add(_Sector5);
            _SectorList.Add(_Sector6);
            _SectorList.Add(_Sector7);
            _SectorList.Add(_Sector8);
            _SectorList.Add(_Sector9);

            var _CSHits = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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

        private static HitChance _ConvertHitchance(int value)
        {
            switch (value)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
                case 4:
                    return HitChance.VeryHigh;
                default:
                    return HitChance.High;
            }
        }

        public static bool CanUseSpell(SpellSlot spell)
        {
            return MyHero.Spellbook.GetSpellState(spell) == SpellState.Ready;
        }

        public static Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
            return tcs.Task;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
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

        public static bool IsTargetValid(AIHeroClient target)
        {
            return target != null && target.IsValidTarget();
        }

        public static bool IsTargetValidWithRange(AIHeroClient target, float range)
        {
            return target != null && target.IsValidTarget(range);
        }

        public static bool IsObjectValid(Obj_AI_Base target)
        {
            return target != null && target.IsValidTarget();
        }

        public static bool IsObjectValidWithRange(Obj_AI_Base target, float range)
        {
            return target != null && target.IsValidTarget(range);
        }
    }
}
