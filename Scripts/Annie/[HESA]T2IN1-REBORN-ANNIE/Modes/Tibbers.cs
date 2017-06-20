using System.Linq;
using System.Collections.Generic;

using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
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
            List<Obj_AI_Minion> _Minions = MinionManager.GetMinions(2000, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health);
            if (_Minions.IsEmpty())
                return;

            Obj_AI_Minion _Target = _Minions.FirstOrDefault();
            if (!_Target.IsValid())
                return;

            if (Globals.MyHero.IsUnderEnemyTurret())
            {
                Obj_AI_Turret _Turret = ObjectManager.Turrets.Enemy.MinOrDefault(x => x.Distance(Globals.MyHero));
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
            }
            else
            {
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
            }
        }

        private static void MethodHealth()
        {
            if (Globals.MyHero.GetEnemiesInRange(2000).Count() == 0) { MethodMinion(); return; }

            AIHeroClient _Target = ObjectManager.Heroes.Enemies.OrderBy(x => x.Health).FirstOrDefault();
            if (!_Target.IsValidTarget(2000))
                return;

            if (AttackUnderTurret)
            {
                if (!_Target.IsUnderEnemyTurret())
                {
                    Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
                }
                else
                {
                    Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, Globals.MyHero);
                }
            }
            else
            {
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
            }
        }

        private static void MethodNearest()
        {
            if (Globals.MyHero.GetEnemiesInRange(2000).Count() == 0) { MethodMinion(); return; }

            AIHeroClient _Target = ObjectManager.Heroes.Enemies.OrderBy(x => x.Distance(Globals.MyHero)).FirstOrDefault();
            if (_Target.IsValidTarget(2000))
                return;

            if (AttackUnderTurret)
            {
                if (!_Target.IsUnderEnemyTurret())
                {
                    Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
                }
                else
                {
                    Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, Globals.MyHero);
                }
            }
            else
            {
                Globals.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Target);
            }
        }

        private static void MethodSelected()
        {
            AIHeroClient _Target = TargetSelector.SelectedTarget;
            if (_Target.IsValidTarget(2000))
            {
                if (Globals.Tibbers.CanAttack)
                {
                    if (AttackUnderTurret)
                    {
                        if (!_Target.IsUnderEnemyTurret())
                        {
                            Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, _Target);
                        }
                        else
                        {
                            Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, Globals.MyHero);
                        }
                    }
                    else
                    {
                        Globals.MyHero.IssueOrder(GameObjectOrder.MovePet, _Target);
                    }
                }
            }
            else
            {
                MethodNearest();
            }
        }
    }
}
