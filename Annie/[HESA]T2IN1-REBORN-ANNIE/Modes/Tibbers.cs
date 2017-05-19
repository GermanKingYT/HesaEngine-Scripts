using System.Collections.Generic;
using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal class Tibbers
    {
        private static readonly bool _AttackUnderTurret = !Menus.ComboMenu.Get<MenuCheckbox>("DontAttackIfUnderTurret").Checked;

        public static void TibbersMethod()
        {
            if (Globals.Tibbers.IsValid())
            {
                switch (Menus.ComboMenu.Get<MenuCombo>("ControlMethodTibbers").CurrentValue)
                {
                    case 1:
                        _MethodHealth();
                        break;
                    case 2:
                        _MethodNearest();
                        break;
                    case 3:
                        _MethodSelected();
                        break;
                    default:
                        _MethodHealth();
                        break;
                }
            }
        }

        private static void _MethodHealth()
        {
            var _Target = ObjectManager.Heroes.Enemies.OrderBy(x => x.Health);
            if (!_Target.IsEmpty())
            {
                var _Sorted = _Target.Where(y => y.ObjectType == GameObjectType.AIHeroClient);
                var _Heroes = _Sorted as IList<AIHeroClient> ?? _Sorted.ToList();
                if (_Heroes.IsEmpty()) return;

                var _Temp = _Heroes.FirstOrDefault();
                if (_Temp != null)
                {
                    if (_Temp.IsValidTarget(1500 /* TODO: Placeholder Value */) && !_Temp.IsDead)
                    {
                        if (_AttackUnderTurret && !_Temp.IsUnderEnemyTurret())
                        {
                            /* TODO: Till "AutoAttackPet" is Fixed */
                            Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Temp));
                            /* Functions.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Sorted.FirstOrDefault()); */
                        }
                        else
                        {
                            /* TODO: Till "AutoAttackPet" is Fixed */
                            Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Temp));
                            /* Functions.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Sorted.FirstOrDefault()); */
                        }
                    }
                }
            }
        }

        private static void _MethodNearest()
        {
            var _Target = ObjectManager.Heroes.Enemies.OrderBy(x => x.Distance(Globals.MyHero));
            if (!_Target.IsEmpty())
            {
                var _Sorted = _Target.Where(y => y.ObjectType == GameObjectType.AIHeroClient);
                var _Heroes = _Sorted as IList<AIHeroClient> ?? _Sorted.ToList();
                if (_Heroes.IsEmpty()) return;

                var _Temp = _Heroes.FirstOrDefault();
                if (_Temp != null)
                {
                    if (_Temp.IsValidTarget(1500 /* TODO: Placeholder Value */) && !_Temp.IsDead)
                    {
                        if (_AttackUnderTurret && !_Temp.IsUnderEnemyTurret())
                        {
                            /* TODO: Till "AutoAttackPet" is Fixed */
                            Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Temp));
                            /* Functions.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Sorted.FirstOrDefault()); */
                        }
                        else
                        {
                            /* TODO: Till "AutoAttackPet" is Fixed */
                            Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Temp));
                            /* Functions.MyHero.IssueOrder(Globals.Tibbers.CanAttack ? GameObjectOrder.AutoAttackPet : GameObjectOrder.MovePet, _Sorted.FirstOrDefault()); */
                        }
                    }
                }
            }
        }

        private static void _MethodSelected()
        {
            var _Target = TargetSelector.SelectedTarget;
            if (_Target.IsValidTarget(1500 /* TODO: Placeholder Value */) && !_Target.IsDead)
            {
                if (Globals.Tibbers.CanAttack)
                {
                    if (_AttackUnderTurret && !_Target.IsUnderEnemyTurret())
                    {
                        /* TODO: Till "AutoAttackPet" is Fixed */
                        Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Target));
                        /* Functions.MyHero.IssueOrder(GameObjectOrder.MovePet, _Target); */
                    }
                    else
                    {
                        /* TODO: Till "AutoAttackPet" is Fixed */
                        Globals.DelayAction(() => SpellsManager.R.CastOnUnit(_Target));
                        /* Functions.MyHero.IssueOrder(GameObjectOrder.MovePet, _Target); */
                    }
                }
            }
            else
            {
                _MethodNearest();
            }
        }
    }
}
