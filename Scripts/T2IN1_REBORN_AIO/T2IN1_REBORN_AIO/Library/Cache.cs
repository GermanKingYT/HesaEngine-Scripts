using System;
using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_AIO.Library
{
    internal static class Cache
    {
        /* Minion Object Lists */
        public static List<Obj_AI_Minion> Minions = new List<Obj_AI_Minion>();
        public static List<Obj_AI_Minion> NeutralMinions = new List<Obj_AI_Minion>();
        public static List<Obj_AI_Minion> EnemyMinions = new List<Obj_AI_Minion>();
        public static List<Obj_AI_Minion> AllyMinions = new List<Obj_AI_Minion>();

        /* Heroe Object Lists */
        public static List<AIHeroClient> Enemies = new List<AIHeroClient>();
        public static List<AIHeroClient> Allies = new List<AIHeroClient>();

        static Cache()
        {
            MinionManager.GetMinions(float.MaxValue).ForEach(SortMinions);

            GameObject.OnCreate += (sender, args) => (sender as Obj_AI_Minion).SortMinions();

            Game.OnUpdate += () =>
            {
                Minions.RemoveAll(x => !x.IsValidEntity());
                NeutralMinions.RemoveAll(x => !x.IsValidEntity());
                EnemyMinions.RemoveAll(x => !x.IsValidEntity());
                AllyMinions.RemoveAll(x => !x.IsValidEntity());

                Enemies = new List<AIHeroClient>(ObjectManager.Heroes.Enemies.Where(x => x.IsValidEntity()));
                Allies = new List<AIHeroClient>(ObjectManager.Heroes.Allies.Where(x => x.IsValidEntity()));
            };
        }

        public static List<Obj_AI_Minion> GetMinions(float range = float.MaxValue, MinionTeam team = MinionTeam.Enemy)
        {
            switch (team)
            {
                case MinionTeam.All:
                    return Minions.FindAll(x => x.IsValidEntity() && x.IsInRange(ObjectManager.Me.Position, range));
                case MinionTeam.Neutral:
                    return NeutralMinions.FindAll(x => x.IsValidEntity() && x.IsInRange(ObjectManager.Me.Position, range));
                case MinionTeam.Enemy:
                    return EnemyMinions.FindAll(x => x.IsValidEntity() && x.IsInRange(ObjectManager.Me.Position, range));
                case MinionTeam.Ally:
                    return AllyMinions.FindAll(x => x.IsValidEntity() && x.IsInRange(ObjectManager.Me.Position, range));
                default:
                    throw new Exception("gj mate you broke it");
            }
        }

        private static void SortMinions(this Obj_AI_Minion minion)
        {
            if (!minion.IsValidEntity() || minion.Team.Equals(GameObjectTeam.Unknown)) return;

            if (minion.Team.Equals(GameObjectTeam.Neutral))
            {
                NeutralMinions.Add(minion);
            }
            else if (minion.Team.Equals(GameObjectTeam.Chaos) || minion.Team.Equals(GameObjectTeam.Order))
            {
                if (minion.Team == ObjectManager.Me.Team)
                {
                    AllyMinions.Add(minion);
                }
                else
                {
                    EnemyMinions.Add(minion);
                }
            }
            else
            {
                Minions.Add(minion);
            }
        }
    }
}