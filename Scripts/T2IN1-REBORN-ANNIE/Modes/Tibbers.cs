using System;
using System.Linq;
using System.Collections.Generic;

using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using T2IN1_REBORN_ANNIE.Managers;

namespace T2IN1_REBORN_ANNIE.Modes
{
    internal class Tibbers
    {
        private static readonly bool AttackUnderTurret = !Menus.ComboMenu.Get<MenuCheckbox>("DontAttackIfUnderTurret").Checked;

        public static void TibbersMethod()
        {
            if (Globals.Tibbers.IsValid())
            {
                switch (Menus.ComboMenu.Get<MenuCombo>("ControlMethodTibbers").CurrentValue)
                {
                    case 1:
                        MethodHealth();
                        break;
                    case 2:
                        MethodNearest();
                        break;
                    case 3:
                        MethodSelected();
                        break;
                    default:
                        MethodHealth();
                        break;
                }
            }
        }

        /* TODO: UNFINISHED */
        private static void MethodMinion()
        {
            IEnumerable<Obj_AI_Minion> minions = Globals.GetLaneMinions(2000);

            if (minions.IsEmpty())
                return;

            Obj_AI_Minion target = minions.FirstOrDefault();

            if (!target.IsValid())
                return;

            if (Globals.MyHero.IsUnderEnemyTurret())
            {
                Obj_AI_Turret _Turret = ObjectManager.Turrets.Enemy.MinOrDefault(x => x.Distance(Globals.MyHero));
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, target);
            }
            else
            {
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, target);
            }
        }

        private static void MethodHealth()
        {
            if (Globals.CachedEnemies == null || !Globals.CachedEnemies.Any(x => x.IsInRange(Globals.MyHero, 2000))) { MethodMinion(); return; }

            Obj_AI_Base target = Globals.CachedEnemies.OrderBy(x => x.Health).FirstOrDefault();

            if (!target.IsValidTarget(2000))
                return;

            if (AttackUnderTurret)
            {
                Core.DelayAction(() => SpellsManager.R.CastOnUnit(!target.IsUnderEnemyTurret() ? target : Globals.MyHero), new Random().Next(300,500));
                /* Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, !target.IsUnderEnemyTurret() ? target : Globals.MyHero); */
            }
            else
            {
                Core.DelayAction(() => SpellsManager.R.CastOnUnit(target), new Random().Next(300, 500));
                /* Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, target); */
            }
        }

        private static void MethodNearest()
        {
            if (Globals.CachedEnemies == null || !Globals.CachedEnemies.Any(x => x.IsInRange(Globals.MyHero, 2000))) { MethodMinion(); return; }

            Obj_AI_Base target = Globals.CachedEnemies.OrderBy(x => x.Distance(Globals.MyHero)).FirstOrDefault();

            if (target.IsValidTarget(2000))
                return;

            if (AttackUnderTurret)
            {
                Core.DelayAction(() => SpellsManager.R.CastOnUnit(!target.IsUnderEnemyTurret() ? target : Globals.MyHero), new Random().Next(300, 500));
                /* Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, !target.IsUnderEnemyTurret() ? target : Globals.MyHero); */
            }
            else
            {
                Core.DelayAction(() => SpellsManager.R.CastOnUnit(target), new Random().Next(300, 500));
                /* Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, target); */
            }
        }

        private static void MethodSelected()
        {
            AIHeroClient target = TargetSelector.SelectedTarget;

            if (target.IsValidTarget(2000))
            {
                if (!Globals.Tibbers.CanAttack) return;

                if (AttackUnderTurret)
                {
                    Core.DelayAction(() => SpellsManager.R.CastOnUnit(!target.IsUnderEnemyTurret() ? target : Globals.MyHero), new Random().Next(300, 500));
                    /* Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, !target.IsUnderEnemyTurret() ? target : Globals.MyHero); */
                }
                else
                {
                    Core.DelayAction(() => SpellsManager.R.CastOnUnit(target), new Random().Next(300, 500));
                    /* Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, target); */
                }
            }
            else
            {
                MethodNearest();
            }
        }
    }
}
